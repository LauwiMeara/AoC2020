using System;
using System.IO;
using System.Linq;

namespace _3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt").ToArray();

            int xMap = input.Length;
            int yMap = input[0].Length;

            char[,] map = MakeMap(input, xMap, yMap);

            Console.WriteLine("The number of trees encountered is {0}.", MoveAndCountTrees(map, xMap, yMap));
        }

        static char[,] MakeMap(string[] input, int xMap, int yMap)
        {
            char[,] map = new char[xMap, yMap];

            for (int i = 0; i < xMap; i++)
            {
                for (int j = 0; j < yMap; j++)
                {
                    map[i, j] = input[i][j];
                }
            }

            return map;
        }

        static int MoveAndCountTrees (char[,] map, int xMap, int yMap)
        {
            int xPosition = 0;
            int yPosition = 0;
            int countedTrees = 0;

            while (xPosition < xMap)
            {
                if (yPosition >= yMap)
                {
                    yPosition = yPosition - yMap;
                }

                if (map[xPosition, yPosition] == '#')
                {
                    countedTrees++;
                }

                xPosition++;
                yPosition += 3;
            }

            return countedTrees;
        }
    }
}
