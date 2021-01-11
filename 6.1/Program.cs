using System;
using System.Collections.Generic;
using System.IO;

namespace _6._1
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("The number of questions answered with \"yes\" is {0}.", GetSumOfTotalAnswers(input));
        }

        static int GetSumOfTotalAnswers(string[] input)
        {
            int counter = 0;

            foreach (string groupInput in input)
            {
                string cleanedGroupInput = groupInput.Replace("\r\n", "");

                counter = counter + GetSumOfGroupAnswers(cleanedGroupInput);
            }

            return counter;
        }

        static int GetSumOfGroupAnswers(string cleanedGroupInput)
        {
            List<char> groupAnswers = new List<char>();

            foreach (char answer in cleanedGroupInput)
            {
                if (!groupAnswers.Contains(answer))
                {
                    groupAnswers.Add(answer);
                }
            }

            return groupAnswers.Count;
        }
    }
}
