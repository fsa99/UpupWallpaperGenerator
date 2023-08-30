namespace MakeUpupResources.CommonBLL
{
    using MakeUpupResources.Helper;
    using MakeUpupResources.Model;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public class VideoFileDragAndDropHandler
    {
        private readonly MainForm _form; // 引用主窗体对象

        public VideoFileDragAndDropHandler(MainForm form)
        {
            _form = form; // 传入主窗体对象，以便在拖放事件中调用其方法
            _form.AllowDrop = true; // 允许窗体接受拖放操作
            _form.DragEnter += Form_DragEnter; // 注册拖入事件处理方法
            _form.DragDrop += Form_DragDrop; // 注册拖放事件处理方法
        }

        // 当拖入对象进入窗体区域时触发
        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) // 如果拖入的是文件
            {
                e.Effect = DragDropEffects.Copy; // 指定拖放效果为复制
            }
        }

        // 当拖入对象在窗体中释放时触发
        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); // 获取拖放的文件路径数组
            if (files.Length > 0)
            {
                string firstFilePath = files[0];
                if (IsVideoFile(firstFilePath))
                {
                    ProcessVideoFile(firstFilePath);
                }
                else
                {
                    MessageBox.Show("拖放的不是视频文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } // 如果有文件被拖放
        }

        private bool IsVideoFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);
            string[] videoExtensions = { ".mp4", ".avi", ".mov" };
            return videoExtensions.Contains(fileExtension.ToLower());
        }
        private void ProcessVideoFile(string videoFilePath)
        {
            string targetPath = AppConfigHelper.GetAppSetting("defaultTargetPath") ?? string.Empty;
            _form.VideoInfo = VideoInfo.CreateBuilder().WithVideoFilePath(videoFilePath).WithTargetPath(targetPath).Build(); 
            _form.DisplayVideoInfo(_form.VideoInfo);
            _form.ProcessVideoFile(videoFilePath);
        }
    }
}
