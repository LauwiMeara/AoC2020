using System;
using System.Collections.Generic;
using System.IO;

namespace _7._2
{
    class Program
    {
        static void Main()
        {
            string[][] rules = GetRules();

            List<string> bagsInShinyGoldBag = new List<string>
            {
                "0 1 shiny gold"
            };

            for (int bagIndex = 0; bagIndex < bagsInShinyGoldBag.Count; bagIndex++)
            {
                AddBagsInShinyGoldBag(rules, bagsInShinyGoldBag, bagIndex);
            }

            int counter = CountBags(bagsInShinyGoldBag);

            Console.WriteLine($"A single shiny gold bag must contain {counter} other bags.");
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

        static void AddBagsInShinyGoldBag(string[][] rules, List<string> bagsInShinyGoldBag, int bagIndex)
        {
            string[] bagParts = bagsInShinyGoldBag[bagIndex].Split(' ');

            for (int i = 0; i < rules.Length; i++)
            {
                string[] ruleParts = rules[i];

                if (ruleParts[0] == bagParts[2] && 
                    ruleParts[1] == bagParts[3])
                {
                    for (int j = 3; j < rules[i].Length; j += 3)
                    {
                        if (ruleParts[j] == "other")
                        {
                            break;
                        }
                        
                        bagsInShinyGoldBag.Add($"{bagIndex} {ruleParts[j - 1]} {ruleParts[j]} {ruleParts[j + 1]}");
                    }
                }
            }
        }

        static int[][] GetSplitBagInfo(List<string> bagsInShinyGoldBag)
        {
            string[][] bagStringInfoParts = new string[bagsInShinyGoldBag.Count][];

            for (int i = 0; i < bagsInShinyGoldBag.Count; i++)
            {
                bagStringInfoParts[i] = bagsInShinyGoldBag[i].Split(' ');
            }

            int[][] bagIntInfoParts = new int[bagStringInfoParts.Length][];

            for (int i = 0; i < bagStringInfoParts.Length; i++)
            {
                bagIntInfoParts[i] = new int[]
                {
                    int.Parse(bagStringInfoParts[i][0]),
                    int.Parse(bagStringInfoParts[i][1])
                };
            }

            return bagIntInfoParts;
        }

        static int GetHighestBagIndex(int[][] bagsIntInfoParts)
        {
            int highestBagIndex = 0;

            foreach (int[] bag in bagsIntInfoParts)
            {
                int bagIndex = bag[0];

                if (bagIndex > highestBagIndex)
                {
                    highestBagIndex = bagIndex;
                }
            }

            return highestBagIndex;
        }

        static int CountBags(List<string> bagsInShinyGoldBag)
        {
            int[][] bagIntInfoParts = GetSplitBagInfo(bagsInShinyGoldBag);

            int highestBagIndex = GetHighestBagIndex(bagIntInfoParts);

            for (int i = highestBagIndex; i >= 0; i--)
            {
                int tempSum = 0;

                for (int j = bagIntInfoParts.Length - 1; j >= 0; j--)
                {
                    int bagIndex = bagIntInfoParts[j][0];
                    int amountOfBags = bagIntInfoParts[j][1];

                    if (bagIndex == i)
                    {
                        tempSum += amountOfBags;
                    }
                }

                bagIntInfoParts[i][1] = bagIntInfoParts[i][1] + (bagIntInfoParts[i][1] * tempSum);
            }

            return bagIntInfoParts[0][1] - 2;
        }
    }
}
