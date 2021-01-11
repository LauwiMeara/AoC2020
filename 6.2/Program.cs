using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _6._2
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("The number of questions everyone in a group answered with \"yes\" is {0}.", GetSumOfTotalAnswers(input));
        }

        static int GetSumOfTotalAnswers(string[] input)
        {
            int counter = 0;

            foreach (string groupInput in input)
            {
                string[] groupInputPerPerson = groupInput.Split("\r\n");

                counter = counter + GetSumOfGroupAnswers(groupInputPerPerson);
            }

            return counter;
        }

        static int GetSumOfGroupAnswers(string[] groupInputPerPerson)
        {
            char[] inputFirstPerson = groupInputPerPerson[0].ToCharArray();

            List<char> groupAnswers = inputFirstPerson.ToList();

            foreach (string personInput in groupInputPerPerson)
            {
                foreach (char answer in inputFirstPerson)
                {
                    if (!personInput.Contains(answer))
                    {
                        groupAnswers.Remove(answer);
                    }

                    if (groupAnswers.Count == 0)
                    {
                        break;
                    }
                }
            }

            return groupAnswers.Count;
        }
    }
}
