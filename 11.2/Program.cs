using System;
using System.IO;

namespace _11._2
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
                        int numFilledSeatsInSight = GetNumFilledSeatsInSight(firstLayout, x, y);

                        if (currentSeat == 'L' && numFilledSeatsInSight == 0) // Empty seats get filled when there are no adjacent occupied seats
                        {
                            secondLayout[x, y] = '#';
                        }
                        else if (currentSeat == '#' && numFilledSeatsInSight >= 5) // Occupied seats become empty when there are five or more occupied seats in sight
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

        static int GetNumFilledSeatsInSight(char[,] firstLayout, int currentX, int currentY)
        {
            int maxX = firstLayout.GetLength(0) - 1;
            int maxY = firstLayout.GetLength(1) - 1;
            int numFilledSeatInSight = 0;

            // Check upper left seat
            int x = currentX;
            int y = currentY;
            bool endLoop = false;

            while (x > 0 && y > 0 && !endLoop)
            {
                switch (firstLayout[x - 1, y - 1])
                {
                    case '#':
                        numFilledSeatInSight++;
                        endLoop = true;
                        break;
                    case 'L':
                        endLoop = true;
                        break;
                    case '.':
                        x--;
                        y--;
                        break;
                }
            }

            // Check upper middle seat
            x = currentX;
            y = currentY;
            endLoop = false;

            while (x > 0 && !endLoop)
            {
                switch (firstLayout[x - 1, y])
                {
                    case '#':
                        numFilledSeatInSight++;
                        endLoop = true;
                        break;
                    case 'L':
                        endLoop = true;
                        break;
                    case '.':
                        x--;
                        break;
                }
            }

            // Check upper right seat
            x = currentX;
            y = currentY;
            endLoop = false;

            while (x > 0 && y < maxY && !endLoop)
            {
                switch (firstLayout[x - 1, y + 1])
                {
                    case '#':
                        numFilledSeatInSight++;
                        endLoop = true;
                        break;
                    case 'L':
                        endLoop = true;
                        break;
                    case '.':
                        x--;
                        y++;
                        break;
                }
            }

            // Check left seat
            x = currentX;
            y = currentY;
            endLoop = false;

            while (y > 0 && !endLoop)
            {
                switch (firstLayout[x, y - 1])
                {
                    case '#':
                        numFilledSeatInSight++;
                        endLoop = true;
                        break;
                    case 'L':
                        endLoop = true;
                        break;
                    case '.':
                        y--;
                        break;
                }
            }

            // Check right seat
            x = currentX;
            y = currentY;
            endLoop = false;

            while (y < maxY && !endLoop)
            {
                switch (firstLayout[x, y + 1])
                {
                    case '#':
                        numFilledSeatInSight++;
                        endLoop = true;
                        break;
                    case 'L':
                        endLoop = true;
                        break;
                    case '.':
                        y++;
                        break;
                }
            }

            // Check lower left seat
            x = currentX;
            y = currentY;
            endLoop = false;

            while (x < maxX && y > 0 && !endLoop)
            {
                switch (firstLayout[x + 1, y - 1])
                {
                    case '#':
                        numFilledSeatInSight++;
                        endLoop = true;
                        break;
                    case 'L':
                        endLoop = true;
                        break;
                    case '.':
                        x++;
                        y--;
                        break;
                }
            }

            // Check lower middle seat
            x = currentX;
            y = currentY;
            endLoop = false;

            while (x < maxX && !endLoop)
            {
                switch (firstLayout[x + 1, y])
                {
                    case '#':
                        numFilledSeatInSight++;
                        endLoop = true;
                        break;
                    case 'L':
                        endLoop = true;
                        break;
                    case '.':
                        x++;
                        break;
                }
            }


            // Check lower right seat
            x = currentX;
            y = currentY;
            endLoop = false;

            while(x < maxX && y < maxY && !endLoop)
            {
                switch (firstLayout[x + 1, y + 1])
                {
                    case '#':
                        numFilledSeatInSight++;
                        endLoop = true;
                        break;
                    case 'L':
                        endLoop = true;
                        break;
                    case '.':
                        x++;
                        y++;
                        break;
                }
            }

            return numFilledSeatInSight;
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
