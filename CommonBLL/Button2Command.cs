namespace MakeUpupResources.CommonBLL
{
    using MakeUpupResources.Helper;
    using Newtonsoft.Json;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    public class Button2Command
    {
        private readonly MainForm _form;

        public Button2Command(MainForm form)
        {
            _form = form;
        }

        public async void Execute()
        {
            if (_form.VideoInfo == null)
            {
                MessageBox.Show("请拖入视频文件！");
                return;
            }
            string targetFolderPath = _form.VideoInfo.TargetPath;
            if (string.IsNullOrEmpty(targetFolderPath))
            {
                MessageBox.Show("目标文件夹路径未配置！");
                return;
            }
            try
            {
                UpupModel upupModel = new UpupModel(_form.VideoInfo.FullPath);
                targetFolderPath = Path.Combine(targetFolderPath, upupModel.themeno);
                Directory.CreateDirectory(targetFolderPath);
                string targetVideoFilePath = Path.Combine(targetFolderPath, _form.VideoInfo.FileName + _form.VideoInfo.Extension);
                File.Move(_form.VideoInfo.FullPath, targetVideoFilePath);

                string jsonFileName = string.IsNullOrEmpty(AppConfigHelper.GetAppSetting("fileName")) ? "theme.upup" : AppConfigHelper.GetAppSetting("fileName");
                string jsonFilePath = Path.Combine(targetFolderPath, jsonFileName);
                string jsonContent = JsonConvert.SerializeObject(upupModel);
                File.WriteAllText(jsonFilePath, jsonContent);

                string imgname = string.IsNullOrEmpty(AppConfigHelper.GetAppSetting("CachingJPGName")) ? "thumbnail.jpg" : AppConfigHelper.GetAppSetting("CachingJPGName");
                string imgOldPath = Path.Combine(Path.Combine(Application.StartupPath, "Image"), imgname);
                if (_form.pictureBoxThumbnail.Image != null && File.Exists(imgOldPath))
                {
                    string imageFileName = string.IsNullOrEmpty(AppConfigHelper.GetAppSetting("JPGName")) ? "private.jpg" : AppConfigHelper.GetAppSetting("JPGName");
                    string imageFilePath = Path.Combine(targetFolderPath, imageFileName);
                    string result = await FileHelper.CopyFileAsync(imgOldPath, imageFilePath);
                    if (string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("处理完成！");
                    }
                    else
                    {
                        LogManager.Instance.Log(NLog.LogLevel.Error, result);
                        MessageBox.Show($"处理出错了{result}");
                    }
                    //// Check if the image format is JPEG
                    //if (_form.pictureBoxThumbnail.Image.RawFormat.Equals(ImageFormat.Jpeg))
                    //{
                    //    using (Image image = _form.pictureBoxThumbnail.Image)
                    //    {
                    //        image.Save(imageFilePath, ImageFormat.Jpeg);
                    //    }
                    //    MessageBox.Show("处理完成！");
                    //}
                    //else
                    //{
                    //    LogManager.Instance.Log(NLog.LogLevel.Info, "Image format is not JPEG.");
                    //}
                }
                else
                {
                    LogManager.Instance.Log(NLog.LogLevel.Info, "Image is null.");
                    MessageBox.Show($"缺少图片！");
                }
                //_form.pictureBoxThumbnail.Image.Save(imageFilePath, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Log(NLog.LogLevel.Error, ex.Message);
                MessageBox.Show($"处理出错：{ex.Message}");
            }
        }
    }

}
