using System;
using System.IO;
using System.Reflection;

namespace FileNameDashRemoverApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start...");
            RemoveDashes();
            Console.WriteLine("End.");
            Console.ReadKey();
        }

        public static bool RemoveDashes()
        {
            try
            {
                var currentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                DirectoryInfo d = new DirectoryInfo(currentDirectory);
                FileInfo[] infos = d.GetFiles();
                foreach (FileInfo f in infos)
                {
                    File.Move(f.FullName, f.FullName.Replace("-", " ").Replace("_"," "));
                }
                Console.WriteLine("Renaming complete...");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex);
                return false;
            }
        }
    }
}
