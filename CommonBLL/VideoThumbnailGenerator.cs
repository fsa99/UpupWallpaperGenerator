namespace MakeUpupResources.CommonBLL
{
    using MakeUpupResources.Helper;
    using MediaToolkit;
    using MediaToolkit.Model;
    using MediaToolkit.Options;
    using System;
    using System.Drawing;
    using System.IO;

    /// <summary>
    /// 用于生成视频缩略图的类。
    /// </summary>
    public class VideoThumbnailGenerator
    {
        public readonly string _tempImagePath;

        public VideoThumbnailGenerator(string tempImagePath)
        {
            _tempImagePath = tempImagePath;
            string folderPath = Path.GetDirectoryName(_tempImagePath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public void ExtractSnapshot(string videoFilePath, TimeSpan position)
        {
            try
            {
                if (!File.Exists(videoFilePath))
                {
                    LogManager.Instance.Log(NLog.LogLevel.Error, $"{videoFilePath} 文件不存在！");
                    return;
                }

                var inputFile = new MediaFile { Filename = videoFilePath };
                if (File.Exists(_tempImagePath))
                {
                    File.Delete(_tempImagePath);
                }
                var outputFile = new MediaFile { Filename = _tempImagePath };

                var conversionOptions = new ConversionOptions { Seek = position };

                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    engine.GetThumbnail(inputFile, outputFile, conversionOptions);
                }
                LogManager.Instance.Log(NLog.LogLevel.Info, $"视频截图成功：{_tempImagePath}");
            }
            catch (Exception ex)
            {
                LogManager.Instance.Log(NLog.LogLevel.Error, $"视频截图失败：{ex.Message}");
            }
        }
    }

}
