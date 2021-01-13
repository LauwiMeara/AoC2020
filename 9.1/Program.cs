using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _9._1
{
    class Program
    {
        static void Main()
        {
            long[] input = File.ReadAllLines("input.txt").Select(long.Parse).ToArray();

            List<long> preamble = new List<long>();

            int preambleLength = 25;

            for (int i = 0; i < preambleLength; i++)
            {
                preamble.Add(input[i]);
            }

            Console.WriteLine("The first number that is not the sum of two of the 25 numbers before it, is {0}.", GetNumWithoutPreambleProperty(preambleLength, input, preamble)); ;
        }

        static long GetNumWithoutPreambleProperty(int preambleLength, long[] input, List<long> preamble)
        {
            long num = 0;
            
            for (int i = preambleLength; i < input.Length; i++)
            {
                for (int j = preamble.Count - 1; j > 0; j--)
                {
                    long remainder = input[i] - preamble[j];
                    int index = preamble.IndexOf(remainder);

                    if (index == j)
                    {
                        break;
                    }
                    else if (preamble.IndexOf(remainder) != -1)
                    {
                        preamble.Add(input[i]);
                        preamble.RemoveAt(0);
                        break;
                    }
                }

                if (preamble.IndexOf(input[i]) == -1)
                {
                    num = input[i];
                    break;
                }
            }

            return num;
        }
    }
}
