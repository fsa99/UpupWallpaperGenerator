namespace MakeUpupResources.Helper
{
    using MakeUpupResources.CommonBLL;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// 辅助类用于管理视频缩略图显示。
    /// </summary>
    public class ThumbnailHelper
    {
        private string _tempImagePath;
        private VideoThumbnailGenerator _thumbnailGenerator;

        public ThumbnailHelper(VideoThumbnailGenerator thumbnailGenerator)
        {
            _thumbnailGenerator = thumbnailGenerator;
            _tempImagePath = _thumbnailGenerator._tempImagePath;
        }

        public void DisplayThumbnail(PictureBox pictureBox, string videoFilePath)
        {
            try
            {
                _thumbnailGenerator.ExtractSnapshot(videoFilePath, TimeSpan.FromSeconds(5)); // 根据需要更改时间间隔

                using (FileStream stream = new FileStream(_tempImagePath, FileMode.Open, FileAccess.Read))
                {
                    Image thumbnail = Image.FromStream(stream);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Image = thumbnail;
                }
            }
            catch (Exception ex)
            {
                LogManager.Instance.Log(NLog.LogLevel.Info, $"无法显示缩略图：{ex.Message}");
            }
            finally
            {
                //if (File.Exists(_tempImagePath))
                //{
                //    File.Delete(_tempImagePath);
                //}
            }
        }
    }

}
