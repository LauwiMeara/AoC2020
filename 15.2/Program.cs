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
            Dictionary<int, int> numbers = GetNumbers(input);

            int spokenNumber = input[^1];

            for (int turn = numbers.Count + 1; turn < 30000000; turn++)
            {
                if (numbers.TryGetValue(spokenNumber, out int value))
                {
                    numbers[spokenNumber] = turn;
                    value = turn - value;
                }
                else
                {
                    numbers.Add(spokenNumber, turn);
                }
                spokenNumber = value;
            }

            Console.WriteLine($"The 30,000,000th number is {spokenNumber}.");
        }

        static Dictionary<int, int> GetNumbers(int[] input)
        {
            Dictionary<int, int> numbers = new Dictionary<int, int>();

            for (int i = 0; i < input.Length - 1; i++)
            {
                numbers.Add(input[i], i + 1);
            }

            return numbers;
        }
    }
}
