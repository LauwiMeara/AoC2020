using System;
using System.Collections.Generic;
using System.IO;

namespace _14._2
{
    class Program
    {
        static void Main()
        {
            string[][] program = GetProgram();

            List<string> usedMemories = new List<string>();
            long sumOfValues = 0;

            // Every mask...
            for (int i = program.Length - 1; i >= 0; i--)
            {
                string mask = program[i][0];

                // ... has an action (memory + value)
                for (int j = program[i].Length - 1; j > 0; j--)
                {
                    char[] memoryInBits = GetMemoryInBits(program, i, j);
                    int value = int.Parse(program[i][j].Substring(program[i][j].LastIndexOf(" ")));

                    // Change bit of memory address to 1 if necessary, and count all bits with X's
                    int xCounter = 0;

                    for (int k = 0; k < 36; k++)
                    {
                        switch (mask[k])
                        {
                            case '0':
                                break;
                            case '1':
                                if (memoryInBits[k] == '0')
                                {
                                    memoryInBits[k] = '1';
                                }
                                break;
                            case 'X':
                                xCounter++;
                                break;
                        }
                    }

                    // Calculate the number of possible permutations
                    double possiblePermutations = Math.Pow(2, xCounter);

                    for (int p = 0; p <= possiblePermutations - 1; p++)
                    {
                        // Get the permutation of the memory address
                        string memoryPermutation = GetPermutedMemory(mask, memoryInBits, xCounter, p);

                        // If the memory address isn't used yet, add to used memories list and add value to sum
                        if (!usedMemories.Contains(memoryPermutation))
                        {
                            usedMemories.Add(memoryPermutation);
                            sumOfValues += value;
                        }
                    }
                }
            }

            Console.WriteLine($"The sum of all values left in memory is {sumOfValues}.");
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

        static char[] GetMemoryInBits(string[][] program, int i, int j)
        {
            int firstIndexMem = program[i][j].IndexOf("[") + 1;
            int lastIndexMem = program[i][j].IndexOf("]");
            int memoryInDecimals = int.Parse(program[i][j].Substring(firstIndexMem, lastIndexMem - firstIndexMem));
            char[] memoryInBits = Convert.ToString(memoryInDecimals, 2).PadLeft(36, '0').ToCharArray();

            return memoryInBits;
        }

        static string GetPermutedMemory(string mask, char[] memoryInBits, int xCounter, int p)
        {
            string str = Convert.ToString(p, 2).PadLeft(xCounter, '0'); // Get binary string
            char[] xArray = new string('0', xCounter).ToCharArray();

            // Use binary string to give the corresponding permutation for the given number of X's
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '1')
                {
                    xArray[i] = '1';
                }
            }

            // Use above permutation of X's to get the permutation of the memory address
            int j = 0;

            while (j < xCounter)
            {
                for (int k = 0; k < 36; k++)
                {
                    if (mask[k] == 'X')
                    {
                        memoryInBits[k] = xArray[j];
                        j++;
                    }
                }
            }

            string memoryPermutation = new string(memoryInBits);

            return memoryPermutation;
        }
    }
}
