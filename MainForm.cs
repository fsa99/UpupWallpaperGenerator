namespace MakeUpupResources
{
    using MakeUpupResources.CommonBLL;
    using MakeUpupResources.Helper;
    using MakeUpupResources.Model;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private readonly VideoFileDragAndDropHandler _dragAndDropHandler;
        private readonly VideoThumbnailGenerator _thumbnailGenerator;
        private readonly ThumbnailHelper _thumbnailHelper;
        /// <summary>
        /// 视频和目标路径
        /// </summary>
        public VideoInfo VideoInfo { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Init();
            string imgname = string.IsNullOrEmpty(AppConfigHelper.GetAppSetting("CachingJPGName")) ? "thumbnail.jpg" : AppConfigHelper.GetAppSetting("CachingJPGName");
            _thumbnailGenerator = new VideoThumbnailGenerator(Path.Combine(Path.Combine(Application.StartupPath, "Image"), imgname));
            _thumbnailHelper = new ThumbnailHelper(_thumbnailGenerator);
            _dragAndDropHandler = new VideoFileDragAndDropHandler(this);
        }

        private void Init()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.textBox1.Text = AppConfigHelper.GetAppSetting("defaultTargetPath") ?? string.Empty;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            using (SolidBrush brush = new SolidBrush(Color.FromArgb((int)(255 * 0.7), this.BackColor)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        /// <summary>
        /// 处理视频文件并显示其缩略图。
        /// </summary>
        /// <param name="videoFilePath">视频文件的路径。</param>
        public void ProcessVideoFile(string videoFilePath)
        {
            _thumbnailHelper.DisplayThumbnail(pictureBoxThumbnail, videoFilePath); // 传递视频文件路径
        }

        public void DisplayVideoInfo(VideoInfo videoInfo)
        {
            labelVideoInfo.Text = $"Full Name: {videoInfo.FullPath}";
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string newTargetPath = textBox1.Text;
            AppConfigHelper.AddOrUpdateAppSetting("defaultTargetPath", newTargetPath);
        }

        /// <summary>
        /// 选择目标目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            FolderDialog openFolder = new FolderDialog();
            if (openFolder.DisplayDialog() == DialogResult.OK)
                textBox1.Text = openFolder.Path.ToString();
            else
                textBox1.Text = "你没有选择目录";
        }

        /// <summary>
        /// 生成upup 资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            Button2Command command = new Button2Command(this);
            command.Execute();
        }
    }
}
