using System;
using System.Text.RegularExpressions;

namespace Task1
{
    class SentanceReverser : TextProcessor
    {
        public static void Reverse(string path, int sentanceNumber)
        {
            Match sentances = Regex.Match(GetText(path), @"[A-Z].*?[\.!?]");
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
}
