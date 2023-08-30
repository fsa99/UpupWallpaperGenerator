namespace MakeUpupResources.CommonBLL
{
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    public class FolderDialog : FolderNameEditor
    {
        FolderBrowser fDialog = new FolderBrowser();
        public FolderDialog()
        {
        }

        /*DisplayDialog(string description)函数用于设置实例fDialog的属性，
        并且显示浏览文件夹对话框。
        为了取得文件夹的路径，设置一个Path属性，返回选取文件夹的路径。*/
        public DialogResult DisplayDialog()
        {
            return DisplayDialog("请选择一个文件夹");
        }

        public DialogResult DisplayDialog(string description)
        {
            fDialog.Description = description;
            return fDialog.ShowDialog();
        }
        public string Path
        {
            get
            {
                return fDialog.DirectoryPath;
            }
        }
        ~FolderDialog()
        {
            fDialog.Dispose();
        }
    }
}
