using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FileNameDashRemoverApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Start...");
            List<string> phrss = new List<string>();

            phrss = TakeOption(phrss);
            //GetPhrases(phrss);
            var res = RemoveDashes(phrss);
            if (res == false)
            {
                Console.WriteLine("No files renamed...");
            }
            Console.WriteLine("End.");
            Console.ReadKey();
        }

        private static List<string> TakeOption(List<string> phrss)
        {
            int option = 1;
            string input;
            string numInput;
            Console.WriteLine("Enter 1 and press enter to remove only '-' symbols.");
            Console.WriteLine("Enter 2 to enter a list of phrases to replace with spaces.");
            Console.WriteLine("Enter 3 to exit.");
            input = Console.ReadLine();
            numInput = Regex.Replace(input, "[^0-9]+", "0");
            option = Convert.ToInt32(numInput);

            if (option == 1)
            {
                phrss.Add("-");

                return phrss;
            }
            else if (option == 2)
            {
                GetPhrases(phrss);
                return phrss;
            }
            else if (option == 3)
            {
                exit(option);
                return phrss;
            }
            else
            {
                Console.WriteLine("Invalid input.");
                TakeOption(phrss);
                return phrss;
            }
        }

        private static void exit(int input)
        {
            if (input == 3)
            {
                System.Environment.Exit(0);
            }
        }

        private static List<string> GetPhrases(List<string> phrss)
        {
            int entKey = (char)13;
            int escKey = (char)27;
            string input;
            string confStr;
            int conf;


            Console.WriteLine("Enter phrase/s to replace. Enter escape to stop entering...");

            //Console.ReadKey(true).Key
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                input = Console.ReadLine();
                phrss.Add(input);
            }

            bool ext;
            do
            {
                input = Console.ReadLine();
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    ext = false;
                    phrss.Add(input);
                }
                else if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    ext = true;
                    break;
                }
            } while (true);






            Console.WriteLine("|------------------ you have entered --------------------------|");

            foreach (var ent in phrss)
            {
                Console.Write(ent + ", ");
            }

            Console.WriteLine("|-------------- press 1 to confirm anything else to re enter, 2 to exit ------------|");

            confStr = Console.ReadLine();

            if ((int.TryParse(confStr, out conf)) && (int.Parse(confStr) == 2))
            {
                GetPhrases(phrss);
            }
            else if (!(int.TryParse(confStr, out conf)) || !(int.Parse(confStr) == 1))
            {
                System.Environment.Exit(0);
            }

            return phrss;
        }

        public static bool RemoveDashes(List<string> phrss)
        {
            try
            {
                if (phrss.Count <= 0)
                {
                    Console.WriteLine("No word/s enter to replace!");
                    return false;
                }
                var currentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                DirectoryInfo d = new DirectoryInfo(currentDirectory);
                FileInfo[] infos = d.GetFiles();
                foreach (FileInfo f in infos)
                {
                    foreach (var phrs in phrss)
                    {
                        File.Move(f.FullName, f.FullName.Replace(phrs, " "));
                    }
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
