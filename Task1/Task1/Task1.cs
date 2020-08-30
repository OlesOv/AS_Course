using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AS_Course
{
    abstract class TextProcessor
    {
        public static string getText(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("This file does not exist");
            }
            return File.ReadAllText(path);
        }
    }

    abstract class TextDeleter : TextProcessor
    {
        public static void DeleteSubstring(string originalPath)
        {
            string originalText = getText(originalPath);

            Console.WriteLine("What to delete: ");
            string deletingString = Console.ReadLine();

            if (originalText.IndexOf(deletingString) < 0) Console.WriteLine("There is no such text!");

            string copyPath = Path.ChangeExtension(originalPath, ".bak" + Path.GetExtension(originalPath));
            File.WriteAllText(copyPath, originalText);

            originalText = originalText.Replace(deletingString, "");
            File.WriteAllText(originalPath, originalText);
            Console.WriteLine("Done!");
        }
    }

    abstract class WordCounter : TextProcessor
    {
        public static void Count(string path)
        {
            Match words = Regex.Match(getText(path), @"\b\w+[-']*\w*\b");
            int counter = 0;
            string selectedWords = "";
            while (words.Success)
            {
                counter++;
                if (counter % 10 == 0)
                {
                    selectedWords += words.Value + ' ';
                }
                words = words.NextMatch();
            }

            Console.WriteLine("There is {0} words in this text, here is every 10th word:\n{1}", counter, selectedWords);
        }
    }

    abstract class SentanceReverser : TextProcessor
    {
        public static void Reverse(string path, int sentanceNumber)
        {
            Match sentances = Regex.Match(getText(path), @"[A-Z].*?[\.!?]");
            while (sentances.Success)
            {
                sentanceNumber--;
                if (sentanceNumber == 0)
                {
                    Match words = Regex.Match(sentances.Value, @"\b\w+[-']*\w*\b");
                    Match notWords = Regex.Match(sentances.Value, @"\W+");
                    string Result = "";
                    while (words.Success)
                    {
                        char[] word = words.Value.ToCharArray();
                        Array.Reverse(word);
                        if (Result.Length == 0) Result = new string(word);
                        else
                        {
                            Result += notWords.Value + new string(word);
                            notWords = notWords.NextMatch();
                        }
                            words = words.NextMatch();
                    }
                    Result += notWords.Value;
                    Console.WriteLine(Result);
                    return;
                }
                sentances = sentances.NextMatch();
            }
        }
    }

    abstract class DirectoryNavigator
    {
        class Comp : IComparer<FileSystemInfo>
        {
            public int Compare(FileSystemInfo x, FileSystemInfo y)
            {
                return String.Compare(x.Name, y.Name);
            }
        }
        public static void StartNavigation(string path)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Directory Navigation!");
            Console.WriteLine(path);
            DirectoryInfo curDir = new DirectoryInfo(path);
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
            if (i == 0) StartNavigation(curDir.Parent.FullName);
            foreach (var p in filesAndFolders)
            {
                i--;
                if (i == 0)
                {
                    if (p.Name.IndexOf('.') < 0) StartNavigation(p.FullName);
                    else Console.WriteLine("It's a file!");
                }
            }
        }
    }

    class Task1
    {
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
                        TextDeleter.DeleteSubstring(path);
                        break;
                    case "2":
                        WordCounter.Count(path);
                        break;
                    case "3":
                        SentanceReverser.Reverse(path, 3);
                        break;
                    case "4":
                        DirectoryNavigator.StartNavigation(path);
                        break;
                    default:
                        Console.WriteLine("Wrong parameter");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Something's wrong. Maybe file or folder does not exist");
            }
            Console.ReadLine();
        }
    }
}
