using System;
using System.IO;

namespace _11._1
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");
            char[,] firstLayout = MakeLayout(input);

            bool isSameLayout = false;

            while (!isSameLayout)
            {
                char[,] secondLayout = GetSecondLayout(firstLayout);

                if (Equals(firstLayout, secondLayout))
                {
                    isSameLayout = true;
                }
                else
                {
                    firstLayout = CopySecondLayoutIntoFirstLayout(firstLayout, secondLayout);
                }
            }

            int numOccupiedSeats = GetNumOccupiedSeats(firstLayout);
            Console.WriteLine($"The number of seats that end up occupied, is {numOccupiedSeats}.");
        }

        static char[,] MakeLayout(string[] input)
        {
            int x = input.Length;
            int y = input[0].Length;

            char[,] layout = new char[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    layout[i, j] = input[i][j];
                }
            }

            return layout;
        }

        static char[,] GetSecondLayout(char[,] firstLayout)
        {
            char[,] secondLayout = new char[firstLayout.GetLength(0), firstLayout.GetLength(1)];

            for (int x = 0; x < firstLayout.GetLength(0); x++)
            {
                for (int y = 0; y < firstLayout.GetLength(1); y++)
                {
                    char currentSeat = firstLayout[x, y];

                    if (currentSeat == '.') // Nothing happens with floor spaces
                    {
                        secondLayout[x, y] = firstLayout[x, y];
                    }
                    else
                    {
                        int numAdjacentFilledSeats = GetNumAdjacentFilledSeats(firstLayout, x, y);

                        if (currentSeat == 'L' && numAdjacentFilledSeats == 0) // Empty seats get filled when there are no adjacent occupied seats
                        {
                            secondLayout[x, y] = '#';
                        }
                        else if (currentSeat == '#' && numAdjacentFilledSeats >= 4) // Occupied seats become empty when there are four or more adjacent occupied seats
                        {
                            secondLayout[x, y] = 'L';
                        }
                        else
                        {
                            secondLayout[x, y] = firstLayout[x, y];
                        }
                    }
                }
            }

            return secondLayout;
        }

        static int GetNumAdjacentFilledSeats(char[,] firstLayout, int x, int y)
        {
            int maxX = firstLayout.GetLength(0) - 1;
            int maxY = firstLayout.GetLength(1) - 1;
            int numAdjacentFilledSeats = 0;

            if (x > 0 && y > 0 && firstLayout[x - 1, y - 1] == '#') // Check upper left seat
            {
                numAdjacentFilledSeats++;
            }
            if (x > 0 && firstLayout[x - 1, y] == '#') // Check upper middle seat
            {
                numAdjacentFilledSeats++;
            }
            if (x > 0 && y < maxY && firstLayout[x - 1, y + 1] == '#') // Check upper right seat
            {
                numAdjacentFilledSeats++;
            }
            if (y > 0 && firstLayout[x, y - 1] == '#') // Check left seat
            {
                numAdjacentFilledSeats++;
            }
            if (y < maxY && firstLayout[x, y + 1] == '#') // Check right seat
            {
                numAdjacentFilledSeats++;
            }
            if (x < maxX && y > 0 && firstLayout[x + 1, y - 1] == '#') // Check lower left seat
            {
                numAdjacentFilledSeats++;
            }
            if (x < maxX && firstLayout[x + 1, y] == '#') // Check lower middle seat
            {
                numAdjacentFilledSeats++;
            }
            if (x < maxX && y < maxY && firstLayout[x + 1, y + 1] == '#') // Check lower right seat
            {
                numAdjacentFilledSeats++;
            }

            return numAdjacentFilledSeats;
        }

        static int GetNumOccupiedSeats(char[,] firstLayout)
        {
            int numOccupiedSeats = 0;

            for (int x = 0; x < firstLayout.GetLength(0); x++)
            {
                for (int y = 0; y < firstLayout.GetLength(1); y++)
                {
                    if (firstLayout[x, y] == '#')
                    {
                        numOccupiedSeats++;
                    }
                }
            }

            return numOccupiedSeats;
        }

        static bool Equals(char[,] firstLayout, char[,] secondLayout)
        {
            bool isEqual = true;

            for (int x = 0; x < firstLayout.GetLength(0); x++)
            {
                if (isEqual == false)
                {
                    break;
                }

                for (int y = 0; y < firstLayout.GetLength(1); y++)
                {
                    if (firstLayout[x, y] != secondLayout[x, y])
                    {
                        isEqual = false;
                        break;
                    }
                }
            }

            return isEqual;
        }

        static char[,] CopySecondLayoutIntoFirstLayout(char[,] firstLayout, char[,] secondLayout)
        {
            for (int x = 0; x < firstLayout.GetLength(0); x++)
            {
                for (int y = 0; y < firstLayout.GetLength(1); y++)
                {
                    firstLayout[x, y] = secondLayout[x, y];
                }
            }

            return firstLayout;
        }
    }
}
