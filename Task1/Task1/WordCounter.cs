using System;
using System.Text.RegularExpressions;

namespace Task1
{
    class WordCounter : TextProcessor
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
}
