using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Reg_Tiktok
{
    public class FileManager
    {
        private static Random rand = new Random();
        public static void WriteFile(string name, string text, bool append = true)
        {
        BACK: ;
            try
            {
                if (append)
                {
                    using (StreamWriter sw = new StreamWriter(name, true, Encoding.UTF8))
                    {
                        sw.WriteLine(text);
                        sw.Close();
                    }
                }
                else
                {
                    File.WriteAllText(name, text);
                }
            }
            catch
            {
                goto BACK;
            }
        }
        public static bool CreateFolder(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void CreateFile(string path)
        {
        BACK: ;
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
                {
                    sw.Close();
                }
            }
            catch
            {
                goto BACK;
            }
        }
        public static string ReadAllText(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch
            {
                return "";
            }
        }
        public static string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }
        public static int CountLines(string path)
        {
            return File.ReadAllLines(path).Length;
        }
        public static string GetPathFileRandomFromPath(string path)
        {
            var files = new DirectoryInfo(path).GetFiles();
            int index = rand.Next(0, files.Length);
            return path + "\\" + files[index].Name;
        }
        public static bool DeleteFolder(string path)
        {
            try
            {
                Directory.Delete(path, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static int CountFolderFromPath(string path)
        {
            try
            {
                return Directory.GetDirectories(path).Length;
            }
            catch
            {
                return 0;
            }
        }
        public static int CountFileFromPath(string path)
        {
            try
            {
                return new DirectoryInfo(path).GetFiles().Length;
            }
            catch
            {
                return 0;
            }
        }
    }
}
