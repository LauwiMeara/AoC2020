using System;
using System.IO;

namespace _4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("The number of valid passports is {0}.", countValidPassports(input));
        }

        static int countValidPassports(string[] input)
        {
            int counter = 0;

            for (int i = 0; i < input.Length; i++)
            {
                string[] requiredFields = { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:"};

                bool isValid = true;

                foreach(string field in requiredFields)
                {
                    isValid = input[i].Contains(field);
                    if (!isValid)
                    {
                        break;
                    }
                }

                if (isValid)
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
