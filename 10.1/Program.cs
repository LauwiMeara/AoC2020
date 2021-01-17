using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10._1
{
    class Program
    {
        static void Main()
        {
            int[] adapters = GetAdapters();

            int counter1JoltDifference = 0;
            int counter2JoltsDifference = 0;
            int counter3JoltsDifference = 0;

            for (int i = 0; i < adapters.Length - 1; i++)
            {
                if (adapters[i + 1] - adapters[i] == 1)
                {
                    counter1JoltDifference++;
                }
                else if (adapters[i + 1] - adapters[i] == 2)
                {
                    counter2JoltsDifference++;
                }
                else if (adapters[i + 1] - adapters[i] == 3)
                {
                    counter3JoltsDifference++;
                }
            }

            Console.WriteLine($"The number of 1 jolt difference is {counter1JoltDifference}.");
            Console.WriteLine($"The number of 3 jolts difference is {counter3JoltsDifference}.");
            Console.WriteLine($"The numbers multiplied is {counter1JoltDifference * counter3JoltsDifference}.");
        }

        static int[] GetAdapters()
        {
            List<int> adapters = File.ReadAllLines("input.txt").Select(int.Parse).ToList();

            adapters.Add(0);
            adapters.Sort();
            adapters.Add(adapters[^1] + 3);

            return adapters.ToArray();
        }
    }
}
