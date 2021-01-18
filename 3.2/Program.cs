using System;
using System.IO;

namespace _3._2
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");

            int xMap = input.Length;
            int yMap = input[0].Length;

            char[,] map = MakeMap(input, xMap, yMap);

            long countedTreesMultiplied = 
                (long)MoveAndCountTrees(map, xMap, yMap, 1, 1) *
                MoveAndCountTrees(map, xMap, yMap, 3, 1) *
                MoveAndCountTrees(map, xMap, yMap, 5, 1) *
                MoveAndCountTrees(map, xMap, yMap, 7, 1) *
                MoveAndCountTrees(map, xMap, yMap, 1, 2);

            Console.WriteLine("If you multiply all the encountered trees, you get {0}.", countedTreesMultiplied);
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
        static int MoveAndCountTrees(char[,] map, int xMap, int yMap, int moveRight, int moveDown)
        {
            int xPosition = 0;
            int yPosition = 0;
            int counter = 0;

            while (xPosition < xMap)
            {
                if (yPosition >= yMap)
                {
                    yPosition -= yMap;
                }

                if (map[xPosition, yPosition] == '#')
                {
                    counter++;
                }

                xPosition += moveDown;
                yPosition += moveRight;
            }

            return counter;
        }
    }
}
