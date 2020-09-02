using System;
using System.Collections.Generic;
using System.IO;

namespace Task1
{
    class DirectoryNavigator
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
}
