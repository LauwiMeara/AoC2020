using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _5._2
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");

            Console.WriteLine("My seat is {0}.", GetMySeat(input));
        }

        static List<int> GetSortedSeatIds(string[] input)
        {
            List<int> seatIds = new List<int>();

            for (int i = 0; i < input.Length; i++)
            {
                char[] boardingPass = input[i].ToCharArray();

                int row = GetRow(boardingPass);
                int column = GetColumn(boardingPass);

                seatIds.Add(row * 8 + column);
            }

            seatIds.Sort();

            return seatIds;
        }

        static int GetRow(char[] boardingPass)
        {
            int minRow = 0;
            int maxRow = 127;

            for (int i = 0; i < 7; i++)
            {
                if (boardingPass[i] == 'F')
                {
                    maxRow = (maxRow - minRow) / 2 + minRow;
                }
                else if (boardingPass[i] == 'B')
                {
                    minRow = (maxRow - minRow) / 2 + 1 + minRow;
                }
            }

            return minRow;
        }

        static int GetColumn(char[] boardingPass)
        {
            int minColumn = 0;
            int maxColumn = 7;

            for (int i = 7; i < 10; i++)
            {
                if (boardingPass[i] == 'L')
                {
                    maxColumn = (maxColumn - minColumn) / 2 + minColumn;
                }
                else if (boardingPass[i] == 'R')
                {
                    minColumn = (maxColumn - minColumn) / 2 + 1 + minColumn;
                }
            }

            return minColumn;
        }

        static int GetMySeat(string[] input)
        {
            List<int> seatIds = GetSortedSeatIds(input);
            
            int lowestSeatId = seatIds[0];
            int highestSeatId = seatIds[^1];

            int[] allPossibleSeatIds = new int[highestSeatId - lowestSeatId];

            for (int i = 0; i < allPossibleSeatIds.Length; i++)
            {
                allPossibleSeatIds[i] = lowestSeatId;
                lowestSeatId++;
            }

            List<int> emptySeats = allPossibleSeatIds.Except(seatIds).ToList();

            return emptySeats[0];
        }
    }
}
