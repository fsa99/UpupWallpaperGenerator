namespace MakeUpupResources.Model
{
    using MakeUpupResources.Helper;
    using System.IO;

    public class VideoInfoBuilder
    {
        private string _videoFilePath;
        private string _targetPath;

        public VideoInfoBuilder WithVideoFilePath(string videoFilePath)
        {
            _videoFilePath = videoFilePath;
            return this;
        }

        public VideoInfoBuilder WithTargetPath(string targetPath)
        {
            _targetPath = targetPath;
            return this;
        }

        public VideoInfo Build()
        {
            string fullPath = _videoFilePath;
            string fileName = Path.GetFileNameWithoutExtension(_videoFilePath);
            string extension = Path.GetExtension(_videoFilePath);
            string directoryPath = Path.GetDirectoryName(_videoFilePath);

            if (string.IsNullOrEmpty(_targetPath))
            {
                return new VideoInfo(fullPath, fileName, extension, directoryPath);
            }
            else
            {
                return new VideoInfo(fullPath, fileName, extension, directoryPath, _targetPath);
            }
        }
    }

    /// <summary>
    /// 路径类
    /// </summary>
    public class VideoInfo
    {
        public string FullPath { get; }
        public string FileName { get; }
        public string Extension { get; }
        public string DirectoryPath { get; }
        public string TargetPath 
        {
            get
            {
                // 从配置文件获取 defaultTargetPath
                return AppConfigHelper.GetAppSetting("defaultTargetPath");
            }
            private set { }
        }

        // 
        public VideoInfo(string fullPath, string fileName, string extension, string directoryPath, string targetPath = null)
        {
            FullPath = fullPath;
            FileName = fileName;
            Extension = extension;
            DirectoryPath = directoryPath;
            TargetPath = targetPath;
        }

        public static VideoInfoBuilder CreateBuilder()
        {
            return new VideoInfoBuilder();
        }

        // 在外部调用时更新 TargetPath 属性的方法
        public void UpdateTargetPath(string newTargetPath)
        {
            TargetPath = newTargetPath;
        }
    }


}
