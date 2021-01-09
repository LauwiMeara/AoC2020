using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2._2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt").ToArray();

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
            int counterValidPassword = 0;

            for (int i = 0; i < splitInput.Count; i++)
            {
                string password = splitInput[i][3];
                char letter = char.Parse(splitInput[i][2]);
                int firstPosition = int.Parse(splitInput[i][0]) - 1;
                int secondPosition = int.Parse(splitInput[i][1]) - 1;

                if ((password[firstPosition] == letter && password[secondPosition] != letter) ||
                    (password[firstPosition] != letter && password[secondPosition] == letter))
                {
                    counterValidPassword++;
                }
            }

            return counterValidPassword;
        }
    }
}
