using System;
using System.Collections.Generic;
using System.IO;

namespace _14._1
{
    class Program
    {
        static void Main()
        {
            string[][] program = GetProgram();

            long[] results = GetResultsList(program);

            long sum = GetSumOfResults(results);

            Console.WriteLine($"The sum of all values in the memory is {sum}.");
        }

        static string[][] GetProgram()
        {
            string[] input = File.ReadAllText("input.txt").Split("mask = ", StringSplitOptions.RemoveEmptyEntries);
            string[][] program = new string[input.Length][];

            for (int i = 0; i < input.Length; i++)
            {
                program[i] = input[i].Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            }

            return program;
        }

        static long[] GetResultsList(string[][] program)
        {
            List<string> usedMemories = new List<string>();
            List<long> results = new List<long>();

            for (int i = program.Length - 1; i >= 0; i--)
            {
                string mask = program[i][0];

                for (int j = program[i].Length - 1; j > 0; j--)
                {
                    string memory = program[i][j].Substring(0, program[i][j].IndexOf(" "));
                    int value = int.Parse(program[i][j].Substring(program[i][j].LastIndexOf(" ")));

                    if (!usedMemories.Contains(memory))
                    {
                        long result = GetResultAfterBitmask(mask, value);

                        results.Add(result);
                        usedMemories.Add(memory);
                    }
                }
            }

            return results.ToArray();
        }

        static long GetResultAfterBitmask(string mask, int value)
        {
            char[] valueIn36Bits = Convert.ToString(value, 2).PadLeft(36, '0').ToCharArray();

            for (int i = 0; i < 36; i++)
            {
                switch (mask[i])
                {
                    case 'X':
                        break;
                    case '0':
                        if (valueIn36Bits[i] == '1')
                        {
                            valueIn36Bits[i] = '0';
                        }
                        break;
                    case '1':
                        if (valueIn36Bits[i] == '0')
                        {
                            valueIn36Bits[i] = '1';
                        }
                        break;
                }
            }

            long result = Convert.ToInt64(new string(valueIn36Bits), 2);

            return result;
        }

        static long GetSumOfResults(long[] results)
        {
            long sum = 0;

            foreach (long result in results)
            {
                sum += result;
            }

            return sum;
        }
    }
}
