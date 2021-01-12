using System;
using System.Collections.Generic;
using System.IO;

namespace _2._1
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");

            char[] separators = { '-', ' ', ':' };

            List<string[]> splitInput = SplitInput(input, separators);

            Console.WriteLine("Number of valid passwords: {0}", CountValidPasswords(splitInput));
        }

        static List<string[]> SplitInput(string[] input, char[] separators)
        {
            List<string[]> splitInput = new List<string[]>();

            for (int i = 0; i < input.Length; i++)
            {
                splitInput.Add(input[i].Split(separators, StringSplitOptions.RemoveEmptyEntries));
            }

            return splitInput;
        }

        static int CountValidPasswords(List<string[]> splitInput)
        {
            int counterLetterInPassword = 0;
            int counterValidPassword = 0;

            for (int i = 0; i < splitInput.Count; i++)
            {
                for (int j = 0; j < splitInput[i][3].Length; j++)
                {
                    if (splitInput[i][3][j] == char.Parse(splitInput[i][2]))
                    {
                        counterLetterInPassword++;
                    }
                }

                if (counterLetterInPassword >= int.Parse(splitInput[i][0]) && counterLetterInPassword <= (int.Parse(splitInput[i][1])))
                {
                    counterValidPassword++;
                }

                counterLetterInPassword = 0;
            }

            return counterValidPassword;
        }
    }
}
