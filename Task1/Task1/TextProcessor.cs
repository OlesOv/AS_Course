using System;
using System.IO;

namespace Task1
{
    abstract class TextProcessor
    {
        public static string GetText(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("This file does not exist");
            }
            return File.ReadAllText(path);
        }
    }
}
