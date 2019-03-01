using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace test
{
 /*
 * 3. Write a program that counts how many times each word from 
 * given text file words.txt appears in it. The character casing 
 * differences should be ignored. The result words should be
 * ordered by their number of occurrences in the text. 
 * 
 * Example:
 *  is -> 2
 *  the -> 2
 *  this -> 3
 *  text -> 6
 */

    public class OccurrencesOfWordsInText
    {
        public static void Main()
        {
            var fileContent = GetFileTextContent("../../words.txt");

            var extractedWords = ExtractWords(fileContent);

            var occurrences = FindElementsOccurrences(extractedWords);

            PrintDictionaryElements(occurrences);

            Console.ReadLine();
        }

        public static IList<string> ExtractWords(string text, string regex = "[a-zA-Z]+")
        {
            var matches = Regex.Matches(text, regex);
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        public static IDictionary<string, int> FindElementsOccurrences(IList<string> collection)
        {
            var dict = new Dictionary<string, int>(new CaseInsensitiveComparer());

            foreach (var t in collection)
            {
                if (!dict.ContainsKey(t))
                {
                    dict[t] = 0;
                }

                dict[t]++;
            }

            var sortedElements = dict.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key.ToLower(), x => x.Value);
            return sortedElements;
        }

        public static void PrintDictionaryElements<T>(IDictionary<T, int> dict)
        {
            foreach (var element in dict)
            {
                Console.WriteLine("{0} -> {1} time(s).", element.Key, element.Value);
            }
        }

        public static string GetFileTextContent(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("File does not exist. File name: " + fullPath);
            }

            string textContent;

            using (var reader = new StreamReader(fullPath))
            {
                textContent = reader.ReadToEnd();
            }

            return textContent;
        }
    }

    public class CaseInsensitiveComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return obj.ToLower().GetHashCode();
        }
    }
}