using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AS_Course
{
    class Program
    {
        static void ReplaceTxt(string originalPath)
        {
            if (!File.Exists(@originalPath))
            {
                Console.WriteLine("This file does not exist");
                return;
            }

            string originalText = File.ReadAllText(@originalPath);

            Console.WriteLine("What to delete: ");
            string replacingString = Console.ReadLine();

            if (originalText.IndexOf(replacingString) < 0) Console.WriteLine("There is no such text!");

            string copyPath = Path.ChangeExtension(originalPath, ".bak" + Path.GetExtension(originalPath));
            File.WriteAllText(@copyPath, originalText);

            originalText = originalText.Replace(replacingString, "");
            File.WriteAllText(@originalPath, originalText);
            Console.WriteLine("Done!");
        }

        static void WordCounter(string path)
        {
            if (!File.Exists(@path))
            {
                Console.WriteLine("This file does not exist");
                return;
            }

            Match words = Regex.Match(File.ReadAllText(@path), @"\b\w+[-']*\w*\b");
            int counter = 0;
            string selectedWords = "";
            while (words.Success)
            {
                counter++;
                if(counter % 10 == 0)
                {
                    selectedWords += words.Value + ' ';
                }
                words = words.NextMatch();
            }

            Console.WriteLine("There is {0} words in this text, here is ebery 10th word:\n{1}",counter, selectedWords);
        }

        static void ReversedSentance(string path)
        {
            if (!File.Exists(@path))
            {
                Console.WriteLine("This file does not exist");
                return;
            }
            int sentanceNumber = 3;

            Match sentances = Regex.Match(File.ReadAllText(@path), @"[A-Z].*?[\.!?]");
            while (sentances.Success)
            {
                sentanceNumber--;
                if (sentanceNumber == 0)
                {
                    Match words = Regex.Match(sentances.Value, @"\b\w+[-']*\w*\b");
                    string Result = "";
                    while (words.Success)
                    {
                        char[] word = words.Value.ToCharArray();
                        Array.Reverse(word);
                        Result += sentances.Value.Substring(Result.Length, words.Index - Result.Length) + new string(word);
                        words = words.NextMatch();
                    }
                    Result += sentances.Value.Substring(Result.Length, sentances.Value.Length - Result.Length);
                    Console.WriteLine(Result);
                    return;
                }
            sentances = sentances.NextMatch();
            }
        }


        public class Comp : IComparer<FileSystemInfo>
        {
            public int Compare(FileSystemInfo x, FileSystemInfo y)
            {
                return String.Compare(x.Name, y.Name);
            }
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
            Array.Sort(filesAndFolders, new Comp());
            foreach (var p in filesAndFolders)
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
            if (args.Length == 0)
            {
                Console.WriteLine("What function do you want to call?");
                functionNumber = Console.ReadLine();
            }
            else
            {
                functionNumber = args[0];
            }
            if (args.Length > 1)
            {
                path = args[1];
            }
            else
            {
                Console.WriteLine("Enter path (start with \\ for relative path): ");
                path = Console.ReadLine();
            }
            if (path[0] == '\\')
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
