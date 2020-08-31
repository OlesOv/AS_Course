using System;
using System.IO;

namespace Task1
{
    class TextDeleter : TextProcessor
    {
        public static void DeleteSubstring(string originalPath)
        {
            string originalText = GetText(originalPath);

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
}
