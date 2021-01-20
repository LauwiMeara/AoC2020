// WARNING: This code takes a long time to get to the end result!

using System;
using System.IO;
using System.Linq;

namespace _13._2
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllText("input.txt").Replace('x', '1').Split(',');

            int[] busIds = GetBusIds(input);

            int highestBusId = busIds.Max();
            int indexHighestBusId = Array.IndexOf(busIds, highestBusId);

            long timeHighestBusId = 100000000000000 - (100000000000000 % highestBusId); // Start around 100000000000000, as the puzzle states that the consecutive timestamp will be higher

            bool isConsecutive = false;

            while (!isConsecutive)
            {
                for (int i = 0; i < busIds.Length; i++)
                {
                    int timeDifference = i - indexHighestBusId;
                    long expectedTime = timeHighestBusId + timeDifference;

                    if (expectedTime % busIds[i] != 0)
                    {
                        timeHighestBusId += highestBusId;
                        break;
                    }

                    if (i == busIds.Length - 1 && expectedTime % busIds[i] == 0)
                    {
                        isConsecutive = true;
                    }
                }
            }

            long consecutiveTimestamp = timeHighestBusId - indexHighestBusId;

            Console.WriteLine($"The consecutive timestamp is {consecutiveTimestamp}.");
        }

        static int[] GetBusIds(string[] input)
        {
            int[] busIds = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                busIds[i] = int.Parse(input[i]);
            }

            return busIds;
        }
    }
}
