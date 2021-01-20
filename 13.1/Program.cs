using System;
using System.IO;
using System.Linq;

namespace _13._1
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");

            int[] busIds = GetBusIds(input);
            int earliestDepartureTime = int.Parse(input[0]);
            
            int[] busWaitingTimes = GetBusWaitingTimes(earliestDepartureTime, busIds);

            int minWaitingTime = busWaitingTimes.Min();
            int busId = busIds[Array.IndexOf(busWaitingTimes, minWaitingTime)];

            Console.WriteLine($"The bus ID is {busId}.");
            Console.WriteLine($"The number of minutes you'll have to wait, is {minWaitingTime}.");
            Console.WriteLine($"These numbers multiplied, is {busId * minWaitingTime}.");
        }

        static int[] GetBusIds(string[] input)
        {
            string[] splitInput = input[1].Replace('x', ',').Split(',', StringSplitOptions.RemoveEmptyEntries);
            int[] busIds = new int[splitInput.Length];

            for (int i = 0; i < splitInput.Length; i++)
            {
                busIds[i] = int.Parse(splitInput[i]);
            }

            return busIds;
        }

        static int[] GetBusWaitingTimes(int earliestDepartureTime, int[] busIds)
        {
            int[] busWaitingTimes = new int[busIds.Length];

            for (int i = 0; i < busIds.Length; i++)
            {
                int remainder = earliestDepartureTime % busIds[i];

                busWaitingTimes[i] = busIds[i] - remainder;
            }

            return busWaitingTimes;
        }
    }
}
