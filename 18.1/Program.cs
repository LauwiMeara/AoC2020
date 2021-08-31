using System;
using System.IO;
using System.Linq;

namespace _18._1
{
    class Program
    {
        static void Main()
        {
            string[][] input = File.ReadAllLines("input.txt").Select(line => line.Replace("(", "( ").Replace(")", " )").Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

            foreach (string[] line in input)
            {
                // Check for brackets.
                while (line.Contains("("))
                {
                    int indexFirstOpenBracket = Array.IndexOf(line, "(");
                    int indexSecondOpenBracket = Array.IndexOf(line, "(", indexFirstOpenBracket + 1);
                    int indexFirstClosedBracket = Array.IndexOf(line, ")");

                    // Check for nested brackets (i.e.: if there is a second open bracket before the first closed bracket).
                    // The expression within the inner brackets should be calculated first.
                    while (indexSecondOpenBracket < indexFirstClosedBracket && indexSecondOpenBracket != -1)
                    {
                        indexFirstOpenBracket = indexSecondOpenBracket;
                        indexSecondOpenBracket = Array.IndexOf(line, "(", indexFirstOpenBracket + 1);
                    }

                    // If the inner expression is found, remove the brackets...
                    line[indexFirstOpenBracket] = "";
                    line[indexFirstClosedBracket] = "";

                    // ... and calculate the expression that stood within the brackets.
                    GetExpressionResult(line, indexFirstOpenBracket, indexFirstClosedBracket);
                }

                // Once all brackets are gone, it is time to calculate the leftover expression of the line.
                GetExpressionResult(line, 0, line.Length);
            }

            string[] flattenInput = input.SelectMany(line => line).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            long sum = flattenInput.Select(value => long.Parse(value)).Sum();
            
            Console.WriteLine($"The total sum is {sum}");
        }

        static void GetExpressionResult(string[] line, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex - 2; i++)
            {
                if (line[i] == "")
                {
                    continue;
                }

                int[] expressionIndexes = GetExpressionIndexes(line, i);

                // If there is only one value left, that is the final value of the line. No more calculations are needed.
                if (expressionIndexes.Length == 1) break;

                line[expressionIndexes[2]] = Calculate(line[expressionIndexes[0]], line[expressionIndexes[1]], line[expressionIndexes[2]]);
                line[expressionIndexes[0]] = "";
                line[expressionIndexes[1]] = "";

                i = expressionIndexes[2] - 1;
            }
        }

        static int[] GetExpressionIndexes(string[] line, int startIndex)
        {
            int indexFirstValue = startIndex;
            int indexOperator = 0;
            int indexSecondValue = 0;

            for (int i = startIndex + 1; i < line.Length - 1; i++)
            {
                if (line[i] != "")
                {
                    indexOperator = i;

                    for (int j = i + 1; j < line.Length; j++)
                    {
                        if (line[j] != "")
                        {
                            indexSecondValue = j;
                            break;
                        }
                    }
                    break;
                }
            }

            // If there is only one value left, only return that value.
            if (indexOperator == 0)
            {
                return new[] { indexFirstValue };
            }
            else
            {
                return new[] { indexFirstValue, indexOperator, indexSecondValue };
            }
        }

        static string Calculate(string a, string opr, string b)
        {
                switch (opr)
                {
                    case "*":
                        return (long.Parse(a) * long.Parse(b)).ToString();
                    case "+":
                        return (long.Parse(a) + long.Parse(b)).ToString();
                    default:
                        throw new ArgumentException("The operator is unknown. Therefore, the expression isn't calculated.");
            }
        }
    }
}
