using System;
using System.IO;

namespace AS_Course
{
    class Program
    {
        static void ReplaceTxt(string originalPath)
        {
            string originalText = File.ReadAllText(@originalPath);

            Console.WriteLine("What to delete: ");
            string replacingString = Console.ReadLine();

            if (originalText.IndexOf(replacingString) < 0) Console.WriteLine("There is no such text!");

            string copyPath = originalPath.Substring(0, @originalPath.Length - 4) + "_backup.txt";
            File.WriteAllText(@copyPath, originalText);

            originalText = originalText.Replace(replacingString, "");
            File.WriteAllText(@originalPath, originalText);
            Console.WriteLine("Done!");
        }

        static void WordCounter(string path)
        {
            char[] separators = { '.', ',', ' ', '\n', '\r', '"' };

            string[] words = File.ReadAllText(@path).Split(separators, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(words.Length);
            string[] selectedWords = new string[words.Length/10];
            int j = 0;
            for (int i = 9; i < words.Length; i += 10)
            {
                selectedWords[j] = words[i];
                j++;
            }
            Console.WriteLine(String.Join(", ", selectedWords));
        }

        static void ReversedSentance(string path)
        {
            int sentanceNumber = 3;

            string text = File.ReadAllText(@path);
            int index = 0;
            while(sentanceNumber > 1)
            {
                index = text.IndexOf(".", index) + 2;
                sentanceNumber--;
            }
            char[] selectedSentance = text.Substring(index, text.IndexOf(".", index) - index + 1).ToCharArray();
            Array.Reverse(selectedSentance);
            Console.WriteLine(new string(selectedSentance));
        }

        static void DirectoryNavigation(string path)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Directory Navigation!");
            Console.WriteLine(@path);
            DirectoryInfo curDir = new DirectoryInfo(@path);
            var filesAndFolders = curDir.GetFileSystemInfos();
            Console.WriteLine("\nID | Name");
            int i = 1;
            foreach(var p in filesAndFolders)
            {
                Console.WriteLine(i + " | " + p.Name);
                i++;
            }
            Console.WriteLine("Enter ID (0 to go up and everything else to exit):");
            try
            {
                i = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Not an ID. Exiting");
            }
            if (i == 0) DirectoryNavigation(curDir.Parent.FullName);
            foreach (var p in filesAndFolders)
            {
                i--;
                if (i == 0)
                {
                    if (p.Name.IndexOf('.') < 0) DirectoryNavigation(p.FullName);
                    else Console.WriteLine("It's a file!");
                }
            }
        }

        static void Main(string[] args)
        {
            string path;
            string functionNumber;
            if(args.Length == 0)
            {
                Console.WriteLine("What function do you want to call?");
                functionNumber = Console.ReadLine();
            }
            else
            {
                functionNumber = args[0];
            }
            if(args.Length > 1)
            {
                path = args[1];
            }
            else
            {
                Console.WriteLine("Enter path (start with \\ for relative path): ");
                path = Console.ReadLine();
            }
            if(path[0] == '\\')
            {
                path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + path;
            }
            try
            {
                switch (functionNumber)
                {
                    case "1":
                        ReplaceTxt(path);
                        break;
                    case "2":
                        WordCounter(path);
                        break;
                    case "3":
                        ReversedSentance(path);
                        break;
                    case "4":
                        DirectoryNavigation(path);
                        break;
                    default:
                        Console.WriteLine("Wrong parameter");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Something's wrong. Probably path");
            }
            Console.ReadLine();
        }
    }
}
