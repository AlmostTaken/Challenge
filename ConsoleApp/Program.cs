using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Write("Enter dictionary path: ");
            var path = Console.ReadLine();
            Console.Write("Find anagrams: ");
            var anagramInput = Console.ReadLine()?.ToLower();
            var watch = Stopwatch.StartNew();
            var resultList = FindAnagrams(path, anagramInput);
            var runTime = watch.Elapsed.TotalMilliseconds * 10000;
            watch.Stop();
            if (resultList.Capacity == 0)
                Console.WriteLine(runTime + ", No anagrams found!");
            else
                Console.WriteLine(runTime + ", " + string.Join(", ", resultList));
            Console.Read();
        }

        public static List<string> FindAnagrams(string path, string anagramInput)
        {
            var anagramsList = new List<string>();
            if (anagramInput == null) return anagramsList;
            var inputOrder = string.Concat(anagramInput.OrderBy(c => c));
            var dictionary = File.ReadAllLines(path ?? throw new InvalidOperationException(),
                Encoding.GetEncoding("iso-8859-13"));
            anagramsList.AddRange(from expression in dictionary
                let expressionLower = expression.ToLower()
                let expressionOrder = string.Concat(expressionLower.OrderBy(c => c))
                where inputOrder == expressionOrder
                select expression);

            return anagramsList;
        }
    }
}