namespace MakeUpupResources
{
    using System;
    using System.IO;
    using MakeUpupResources.Helper;
    using MediaToolkit;
    using MediaToolkit.Model;
    using MediaToolkit.Options;

    /// <summary>
    /// 视屏快照 图片
    /// </summary>
    public class VideoSnapshotHelper
    {
        /// <summary>
        /// 从视频文件中提取指定时间点的截图，并保存为图像文件。
        /// </summary>
        /// <param name="videoFilePath">视频文件的路径。</param>
        /// <param name="outputImagePath">保存截图的图像文件路径。</param>
        /// <param name="position">要提取截图的时间点。</param>
        public static void ExtractSnapshot(string videoFilePath, string outputImagePath, TimeSpan position)
        {
            try
            {
                if (!File.Exists(videoFilePath))
                {
                    Console.WriteLine("文件不存在！！！");
                    return;
                }
                var inputFile = new MediaFile { Filename = videoFilePath };
                var outputFile = new MediaFile { Filename = outputImagePath };

                // 创建 ConversionOptions 对象，并设置截图的时间点
                var conversionOptions = new ConversionOptions { Seek = position };

                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);

                    // 设置截图的时间点
                    engine.GetThumbnail(inputFile, outputFile, conversionOptions);
                }

                LogManager.Instance.Log(NLog.LogLevel.Info, $"视频截图成功：{outputImagePath}");
            }
            catch (Exception ex)
            {
                LogManager.Instance.Log(NLog.LogLevel.Error, $"视频截图失败：{ex.Message}");
            }
        }
    }
}
