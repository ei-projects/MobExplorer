using System.IO;
using System.Reflection;

namespace GuiLib
{
    public class GuiHelper
    {
        static string prevDir;
        static public string getSelfPath()
        {
            //string fullPath = Application.ExecutablePath;
            //int pos = fullPath.LastIndexOf('\\');
            //fullPath = fullPath.Substring(0, pos);
            //return fullPath;
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        static public void setSelfDir()
        {
            prevDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(getSelfPath());
        }
        static public void restorePrevDir()
        {
            Directory.SetCurrentDirectory(prevDir);
        }

    }

}
