using System;
using System.IO;

namespace Task1
{
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
