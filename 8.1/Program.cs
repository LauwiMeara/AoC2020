using System;
using System.Collections.Generic;
using System.IO;

namespace _8._1
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");

            Console.WriteLine("The value of the accumulator before the second loop is {0}.", GetAccumulator(input));
        }

        static int GetAccumulator (string[] input)
        {
            List<int> runInstructions = new List<int>();

            int index = 0;
            int acc = 0;

            while (!runInstructions.Contains(index))
            {
                char operation = input[index][0];

                int argument = int.Parse(input[index].Substring(4));

                runInstructions.Add(index);

                if (operation == 'a')
                {
                    acc += argument;
                    index++;
                }
                else if (operation == 'j')
                {
                    index += argument;
                }
                else if (operation == 'n')
                {
                    index++;
                }
            }

            return acc;
        }
    }
}
