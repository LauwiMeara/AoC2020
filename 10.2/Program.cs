using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10._2
{
    class Program
    {
        static void Main()
        {
            int[] joltDifferences = GetJoltDifferences();

            long distinctArrangements = 1;

            for (int i = 0; i < joltDifferences.Length; i++)
            {
                if (joltDifferences[i] == 1) // If the jolt difference is 1, start counting the series.
                {
                    int seriesCounter = 1;

                    for (int j = i + 1; j < joltDifferences.Length; j++)
                    {
                        if (joltDifferences[j] == 1)
                        {
                            seriesCounter++;
                        }
                        else
                        {
                            int optionsForSeries = GetOptionsForSeries(seriesCounter);

                            distinctArrangements *= optionsForSeries; // The options for series can be multiplied by each other to get the total of distinct arrangements.

                            i = j;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"The number of distinct arrangements is {distinctArrangements}.");
        }

        static int[] GetJoltDifferences()
        {
            int[] adapters = GetAdapters();

            int[] joltDifferences = new int[adapters.Length - 1];

            for (int i = 0; i < adapters.Length - 1; i++)
            {
                joltDifferences[i] = adapters[i + 1] - adapters[i];
            }

            return joltDifferences;
        }

        static int[] GetAdapters()
        {
            List<int> adapters = File.ReadAllLines("input.txt").Select(int.Parse).ToList();

            adapters.Add(0);
            adapters.Sort();
            adapters.Add(adapters[^1] + 3);

            return adapters.ToArray();
        }

        static int GetOptionsForSeries(int seriesCounter)
        {
            // One 1 jolt difference can't be replaced.
            // Two 1 jolt differences can be replaced: 1 1 can be 2 (2 options)
            // Three 1 jolt differences can be replaced: 1 1 1 can be 1 2, 2 1 or 3 (4 options).
            // Four 1 jolt differences can be replaced: 1 1 1 1 can be 1 1 2, 1 2 1, 2 1 1, 1 3, 3 1 or 2 2 (7 options).
            // Five 1 jolt differences can be replaced: 1 1 1 1 1 can be 1 1 1 2, 1 1 2 1, 1 2 1 1, 2 1 1 1, 1 2 2, 2 1 2, 2 2 1, 1 1 3, 1 3 1, 3 1 1, 2 3, 3 2 (13 options).

            int optionsForSeries = 1;

            switch (seriesCounter)
            {
                case 1:
                    break;
                case 2:
                    optionsForSeries = 2;
                    break;
                case 3:
                    optionsForSeries = 4;
                    break;
                case 4:
                    optionsForSeries = 7;
                    break;
                case 5:
                    optionsForSeries = 13;
                    break;
                default:
                    Console.WriteLine("The series consists out of more than five 1 jolt differences. Please review the code.");
                    break;
            }

            return optionsForSeries;
        }
    }
}
