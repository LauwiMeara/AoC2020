using System;
using System.IO;
using System.Linq;

namespace _1._2
{
    class Program
    {
        static void Main()
        {
            int[] entries = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();

            Search2020(entries);
        }

        static void Search2020(int[] entries)
        {
            for (int i = 0; i < entries.Length - 2; i++)
            {
                for (int j = i; j < entries.Length - 1; j++)
                {
                    for (int k = j; k < entries.Length; k++)
                    {
                        if (entries[i] + entries[j] + entries[k] == 2020)
                        {
                            Console.WriteLine("{0} * {1} * {2} = {3}", entries[i], entries[j], entries[k], entries[i] * entries[j] * entries[k]);
                            break;
                        }
                    }
                }
            }
        }
    }
}
