namespace MakeUpupResources
{
    using MakeUpupResources.Helper;
    using System;
    using System.IO;

    public class UpupModel
    {
        /// <summary>
        /// 使用者 名字
        /// </summary>
        public string UserName { get; set; } = AppConfigHelper.GetAppSetting("UserName") ?? "小怪兽";

        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; } = AppConfigHelper.GetAppSetting("author") ?? "小怪兽";

        /// <summary>
        /// 作品名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 视屏地址
        /// </summary>
        public string src { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        public string tag { get; set; } = AppConfigHelper.GetAppSetting("tag") ?? "6";

        /// <summary>
        /// 用户界面类型
        /// </summary>
        public string themeType { get; set; } = AppConfigHelper.GetAppSetting("themeType") ?? "1";

        /// <summary>
        /// 主题编号
        /// </summary>
        public string themeno { get; set; } = $"{DateTime.Now.ToString("yyMMddHHmmss")}";

        /// <summary>
        /// 完整路径
        /// </summary>
        public string url { get; set; } = AppConfigHelper.GetAppSetting("url") ?? "http://upupoo-video-mp4.7way.cn//theme";
        /// <summary>
        /// 已使用
        /// </summary>
        public string used { get; set; } = AppConfigHelper.GetAppSetting("used") ?? "1";
        /// <summary>
        /// 版本
        /// </summary>
        public string ver { get; set; } = AppConfigHelper.GetAppSetting("ver") ?? "1";

        public UpupModel(string videoFilePath)
        {
            name = Path.GetFileNameWithoutExtension(videoFilePath);
            src = Path.GetFileName(videoFilePath);
            url += "//" + this.themeno + "//" + AppConfigHelper.GetAppSetting("fileName") ?? "theme.upup"; ;
        }
    }
}
