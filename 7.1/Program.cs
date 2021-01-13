using System;
using System.Collections.Generic;
using System.IO;

namespace _7._1
{
    class Program
    {
        static void Main()
        {
            string[][] rules = GetRules();

            List<string> bagsHoldingShinyGoldBag = new List<string>
            {
                "shiny gold"
            };

            for (int bagIndex = 0; bagIndex < bagsHoldingShinyGoldBag.Count; bagIndex++)
            {
                AddBagsHoldingShinyGoldBag(rules, bagsHoldingShinyGoldBag, bagIndex);
            }

            int counter = bagsHoldingShinyGoldBag.Count - 1;

            Console.WriteLine($"The number of bags that can hold a shiny gold bag, is {counter}.");
        }

        static string[][] GetRules()
        {
            string[] input = File.ReadAllLines("input.txt");

            string[][] rules = new string[input.Length][];

            for (int i = 0; i < input.Length; i++)
            {
                string[] separators = { "bags", "bag", "contain", ",", ".", " " };

                rules[i] = input[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }

            return rules;
        }

        static void AddBagsHoldingShinyGoldBag(string[][] rules, List<string> bagsHoldingShinyGoldBag, int bagIndex)
        {
            string[] bagParts = bagsHoldingShinyGoldBag[bagIndex].Split(' ');

            for (int i = 0; i < rules.Length; i++)
            {
                for (int j = 3; j < rules[i].Length; j += 3)
                {
                    if (rules[i][j] == "other")
                    {
                        break;
                    }
                    else if (rules[i][j] == bagParts[0] &&
                        rules[i][j + 1] == bagParts [1] &&
                        !bagsHoldingShinyGoldBag.Contains($"{rules[i][0]} {rules[i][1]}"))
                    {
                        bagsHoldingShinyGoldBag.Add($"{rules[i][0]} {rules[i][1]}");
                    }
                }
            }
        }
    }
}