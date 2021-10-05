using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace _19._1
{
    class Program
    {
        // NOTE:
        // The code below first collects all message options before comparing them to the received messages.
        // This method works with the test input, but it requires too much time and memory with the real input.
        // Therefore, the puzzle remains unsolved.

        // An alternative would be to review all received messages per character and to decide for each substring if the message could still be valid or not.
        // This is something to look into later.

        static void Main()
        {
            // Divide input in rules and messages
            string[] input = File.ReadAllText("input.txt").Replace("\\", string.Empty).Replace("\"", string.Empty).Split($"{Environment.NewLine}{Environment.NewLine}");
            Dictionary<int, string[]> rules = GetRules(input);
            string[] receivedMessages = input[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // For each key, transform the value to the message. Continue doing this until key 0 only contains a message
            char[] validChars = new char[] { '|', '(', ')', ' ' };
            while (rules[0].Any(value => value.Any(c => !char.IsLetter(c) && !validChars.Contains(c))))
            {
                TransformRulesToMessages(rules, validChars, 0);
            }

            // Start an ordered dictionary with rule 0
            OrderedDictionary messageOptions = new OrderedDictionary
            {
                { string.Join("", rules[0]), 0 }
            };

            // Collect all message options
            int i = 0;
            while (i < messageOptions.Count)
            {
                if (messageOptions.Cast<DictionaryEntry>().ElementAt(i).Key.ToString().Contains("("))
                {
                    CollectMessageOptions(messageOptions, i);
                }
                else
                {
                    i++;
                }
            }

            // Check how many received messages are valid
            int counter = 0;
            foreach (string message in receivedMessages)
            {
                if (messageOptions.Contains(message))
                {
                    counter++;
                }
            }

            Console.WriteLine($"{counter} received messages are valid");
        }

        static Dictionary<int, string[]> GetRules(string[] input)
        {
            string[][] inputRules = input[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(line => line.Split(":", StringSplitOptions.RemoveEmptyEntries)).ToArray();
            Dictionary<int, string[]> rules = new Dictionary<int, string[]>();

            for (int i = 0; i < inputRules.Length; i++)
            {
                rules.Add(int.Parse(inputRules[i][0]), inputRules[i][1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray());
            }

            return rules;
        }

        static void TransformRulesToMessages(Dictionary<int, string[]> rules, char[] validChars, int key)
        {
            for (int i = 0; i < rules[key].Length; i++)
            {
                // Check value of key
                string value = rules[key][i];

                // If value of key is not a letter or valid character, check the key the value refers to
                if (!value.All(c => char.IsLetter(c) || validChars.Contains(c)))
                {
                    int keyRef = int.Parse(value);
                    string[] valuesRef = rules[keyRef];

                    // If all referred values are letters or valid characters, replace them as value of the key
                    if (valuesRef.All(value => value.All(c => char.IsLetter(c) || validChars.Contains(c))))
                    {
                        // If there are multiple options (|), place the string between brackets
                        if (valuesRef.Contains("|"))
                        {
                            rules[key][i] = "(" + string.Join("", valuesRef) + ")";
                        }
                        else
                        {
                            rules[key][i] = string.Join("", valuesRef);
                        }
                    }
                    // Otherwise, recall this function with the referred key
                    else
                    {
                        TransformRulesToMessages(rules, validChars, keyRef);
                    }
                }
            }
        }

        static void CollectMessageOptions(OrderedDictionary messageOptions, int i)
        {
            string message = messageOptions.Cast<DictionaryEntry>().ElementAt(i).Key.ToString();
            int indexFirstOpenBracket = message.IndexOf('(');
            int indexSecondOpenBracket = message.IndexOf('(', indexFirstOpenBracket + 1);
            int indexFirstClosedBracket = message.IndexOf(')');

            // Check for nested brackets (i.e.: if there is a second open bracket before the first closed bracket).
            // The expression within the inner brackets should be dissolved first
            while (indexSecondOpenBracket < indexFirstClosedBracket && indexSecondOpenBracket != -1)
            {
                indexFirstOpenBracket = indexSecondOpenBracket;
                indexSecondOpenBracket = message.IndexOf('(', indexFirstOpenBracket + 1);
            }

            // If the inner expression is found, get the substring
            string messageSubstring = message.Substring(indexFirstOpenBracket, (indexFirstClosedBracket - indexFirstOpenBracket + 1));

            // Remove the brackets and split the substring per option (|)
            string[] messageSubstringArr = messageSubstring.Replace("(", string.Empty).Replace(")", string.Empty).Split("|");

            // Add new options to the dictionary of valid messages
            foreach (string substring in messageSubstringArr)
            {
                string result = message.Substring(0, indexFirstOpenBracket) + substring + message.Substring(indexFirstClosedBracket + 1);
                if (!messageOptions.Contains(result))
                {
                    messageOptions.Add(result, 0);
                }
            }

            // Remove checked index in the dictionary
            messageOptions.RemoveAt(i);
        }
    }
}

