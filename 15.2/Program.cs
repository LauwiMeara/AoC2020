// WARNING: This code takes a long time to get to the end result!
// It did work for turn 2,020, but I'm not sure yet if it works for turn 30,000,000.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _15._2
{
    class Program
    {
        static void Main()
        {
            int[] input = File.ReadAllText("input.txt").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            
            List<int> numbers = GetListOfNumbers(input);

            int turn = numbers.Count + 1;
            int lastNumber = input[^1];

            while (turn < 30000000)
            {
                int age;
                int index;

                if (!numbers.Contains(lastNumber))
                {
                    age = 0;
                    numbers.Add(lastNumber);
                    lastNumber = age;
                }
                else
                {
                    index = numbers.LastIndexOf(lastNumber);
                    age = (turn - 1) - index;
                    numbers.Add(lastNumber);
                    lastNumber = age;
                }

                turn++;
            }

            Console.WriteLine($"The 30,000,000th number is {lastNumber}.");
        }

        static List<int> GetListOfNumbers(int[] input)
        {
            List<int> numbers = new List<int>();

            for (int i = 0; i < input.Length - 1; i++)
            {
                numbers.Add(input[i]);
            }

            return numbers;
        }
    }
}
