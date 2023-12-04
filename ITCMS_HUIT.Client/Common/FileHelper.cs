namespace ITCMS_HUIT.Client.Common
{
    public static class FileHelper
    {
        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }
    }
}
