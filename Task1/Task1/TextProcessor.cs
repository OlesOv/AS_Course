using System;
using System.IO;

namespace Task1
{
    class TextProcessor
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
}
