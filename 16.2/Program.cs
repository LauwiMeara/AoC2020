using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _16._2
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, int[]> rules = GetRules();
            int[] myTicket = File.ReadAllText("myTicket.txt").Split(',').Select(int.Parse).ToArray();
            int[][] tickets = File.ReadAllLines("tickets.txt").Select(ticket => ticket.Split(',').Select(int.Parse).ToArray()).ToArray();

            // Remove invalid tickets
            HashSet<int> values = GetValues(rules);
            int[][] filteredTickets = tickets.Where(ticket => ticket.All(ticketValue => values.Contains(ticketValue))).ToArray();

            // Get all the possible positions per field name
            Dictionary<string, List<int>> possibleFieldPositions = GetPossibleFieldPositions(rules, filteredTickets);

            // Get the definite position per field name
            Dictionary<string, int> definiteFieldPositions = GetDefiniteFieldPosition(rules, possibleFieldPositions);

            // Get multiplication of all "departure" values on my ticket
            long result = 1;

            foreach (KeyValuePair<string, int> field in definiteFieldPositions)
            {
                if (field.Key.Contains("departure"))
                {
                    result *= myTicket[field.Value];
                }
            }

            Console.WriteLine($"The result is {result}.");
        }

        static Dictionary<string, int[]> GetRules()
        {
            string[][] input = File.ReadAllLines("rules.txt").Select(rule => rule.Split(new string[] { ": ", "-", " or " }, StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
            Dictionary<string, int[]> rules = new Dictionary<string, int[]>();

            foreach (string[] rule in input)
            {
                rules.Add(rule[0], new int[] { int.Parse(rule[1]), int.Parse(rule[2]), int.Parse(rule[3]), int.Parse(rule[4])});
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

                for (int value = rule.Value[2]; value < rule.Value[3]; value++)
                {
                    values.Add(value);
                }
            }

            return values;
        }

        static Dictionary<string, List<int>> GetPossibleFieldPositions(Dictionary<string, int[]> rules, int[][] filteredTickets)
        {
            Dictionary<string, List<int>> possibleFieldPositions = new Dictionary<string, List<int>>();
            string[] fieldNames = rules.Keys.ToArray();

            // Check for each field name if a certain position on all the tickets is part of the given ranges for the field
            // If true, add position to the list of possible positions for the field
            foreach (string fieldName in fieldNames)
            {
                List<int> positions = new List<int>();

                for (int position = 0; position < rules.Count; position++)
                {
                    for (int ticket = 0; ticket < filteredTickets.Length; ticket++)
                    {
                        int result = filteredTickets[ticket][position];

                        if ((result >= rules[fieldName][0] && result <= rules[fieldName][1]) || (result >= rules[fieldName][2] && result <= rules[fieldName][3]))
                        {
                            if (ticket == filteredTickets.Length - 1)
                            {
                                positions.Add(position);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                possibleFieldPositions.Add(fieldName, positions);
            }

            return possibleFieldPositions;
        }

        static Dictionary<string, int> GetDefiniteFieldPosition(Dictionary<string, int[]> rules, Dictionary<string, List<int>> possibleFieldPositions)
        {
            Dictionary<string, int> definiteFieldPositions = new Dictionary<string, int>();

            // While not all positions are determined, search for the field that only has one possible position
            while (definiteFieldPositions.Count < rules.Count)
            {
                foreach (KeyValuePair<string, List<int>> field in possibleFieldPositions)
                {
                    // If field with only one possible position, save this position for this field
                    // Remove this position as a possible position for the other fields
                    if (field.Value.Count == 1)
                    {
                        int column = field.Value[0];
                        definiteFieldPositions.Add(field.Key, column);

                        foreach (KeyValuePair<string, List<int>> field2 in possibleFieldPositions)
                        {
                            if (field2.Value.Contains(column))
                            {
                                field2.Value.Remove(column);
                            }
                        }
                    }
                }
            }

            return definiteFieldPositions;
        }
    }
}
