using System;
using System.IO;
using System.Linq;

namespace _1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] entries = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();

            Search2020(entries);
        }

        static void Search2020(int[] entries)
        {
            for (int i = 0; i < entries.Length - 1; i++)
            {
                for (int j = i; j < entries.Length; j++)
                {
                    if (entries[i] + entries[j] == 2020)
                    {
                        Console.WriteLine("{0} * {1} = {2}", entries[i], entries[j], entries[i] * entries[j]);
                        break;
                    }
                }
            }
        }
    }
}
