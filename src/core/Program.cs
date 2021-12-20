using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace poc.brack.list
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<int> ids = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 10, 11, 12, 13, 14, 15, 16, 17, 18, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30,  };

            var limit = 90_000;
            var ids = Enumerable.Range(0, limit).ToList();

            var result = splitList<int>(ids, 500);

            var list1 = result[0];

            var list2 = result[1];

            var i = 0;

            var noParallel = Stopwatch.StartNew();
            foreach (var item in ids)
            {
                Console.Write($"{item} ");
            }
            noParallel.Stop();

            Console.WriteLine("\n" + noParallel.ElapsedMilliseconds + "ms");
            Console.WriteLine("");

            var ParallelFor = Stopwatch.StartNew();

            Parallel.For(0, result.Count, index =>
            {
                Parallel.ForEach(result[index], item =>
                {
                    Console.Write($"{item} ");
                });

                i++;
            });

            ParallelFor.Stop();

            Console.WriteLine("\n" + ParallelFor.ElapsedMilliseconds + "ms");

            Console.ReadLine();
        }

        public static List<List<T>> splitList<T>(List<T> locations, int nSize = 10)
        {
            var list = new List<List<T>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }

            return list;
        }
    }

    
}
