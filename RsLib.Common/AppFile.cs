using System.IO;
namespace RsLib.Common
{
    public static class AppFolderHandle
    {
        public static string Folder_App => System.Environment.CurrentDirectory;
        public static string Folder_Config
        {
            get
            {
                string folder = $"{Folder_App}\\Config";
                if (Directory.Exists(folder) == false) Directory.CreateDirectory(folder);
                return folder;
            }
        }
    }
}
