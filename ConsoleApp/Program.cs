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
            List<string> anagramsList = new List<string>();
            if (anagramInput != null)
            {
                var inputOrder = string.Concat(anagramInput.OrderBy(c => c));
                var dictionary = File.ReadAllLines(path ?? throw new InvalidOperationException(), Encoding.GetEncoding("iso-8859-13"));
                foreach (var expression in dictionary)
                {
                    var expressionLower = expression.ToLower();
                    var expressionOrder = string.Concat(expressionLower.OrderBy(c => c));
                    if (inputOrder == expressionOrder)
                    {
                        anagramsList.Add(expression);
                    }
                }
            }

            var runTime = watch.Elapsed.TotalMilliseconds * 10000;
            watch.Stop();
            if (anagramsList.Capacity == 0)
            {
                Console.WriteLine(runTime + ", No anagrams found!");
            }
            else
            {
                Console.WriteLine(runTime + ", " + string.Join(", ", anagramsList));
            }
        }
    }
}