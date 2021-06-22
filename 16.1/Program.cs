using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _16._1
{
    class Program
    {
        static void Main()
        {
            HashSet<int> values = GetValues(GetRules());

            int[][] tickets = File.ReadAllLines("tickets.txt").Select(ticket => ticket.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();
            int[] flattenTickets = tickets.SelectMany(ticketValue => ticketValue).ToArray();

            int errorRate = 0;

            foreach (int ticketValue in flattenTickets)
            {
                if (!values.Contains(ticketValue))
                {
                    errorRate += ticketValue;
                }
            }

            Console.WriteLine($"The ticket scanning error rate is {errorRate}.");
        }

        static Dictionary<string, int[]> GetRules()
        {
            string[][] input = File.ReadAllLines("rules.txt").Select(rule => rule.Split(new string[] { ": ", "-", " or " }, StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
            Dictionary<string, int[]> rules = new Dictionary<string, int[]>();

            foreach (string[] rule in input)
            {
                int suffix = 0;
                for (int i = 1; i < rule.Length; i += 2)
                {
                    rules.Add(rule[0] + suffix, new int[] { int.Parse(rule[i]), int.Parse(rule[i + 1]) });
                    suffix++;
                }
            }

            return rules;
        }

        static HashSet<int> GetValues(Dictionary<string, int[]> rules)
        {
            HashSet<int> values = new HashSet<int>();
            foreach (KeyValuePair<string, int[]> rule in rules)
            {
                for (int value = rule.Value[0]; value <= rule.Value[1]; value++)
                {
                    values.Add(value);
                }
            }

            return values;
        }
    }
}
