using System;
using System.IO;

namespace _12._1
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");

            int[] location = GetNewLocation(input);

            int manhattanDistance = Math.Abs(location[0]) + Math.Abs(location[1]);
            Console.WriteLine($"The Manhattan distance is {manhattanDistance}.");
        }

        static int[] GetNewLocation(string[] input)
        {
            char facing = 'E';

            int[] location = new int[] { 0, 0 };

            foreach (string instruction in input)
            {
                char action = instruction[0];
                int value = int.Parse(instruction.Substring(1));

                switch (action)
                {
                    case 'N':
                        location[0] += value;
                        break;
                    case 'S':
                        location[0] -= value;
                        break;
                    case 'E':
                        location[1] += value;
                        break;
                    case 'W':
                        location[1] -= value;
                        break;
                    case 'L':
                        facing = TurnLeft(facing, value);
                        break;
                    case 'R':
                        facing = TurnRight(facing, value);
                        break;
                    case 'F':
                        switch (facing)
                        {
                            case 'N':
                                location[0] += value;
                                break;
                            case 'S':
                                location[0] -= value;
                                break;
                            case 'E':
                                location[1] += value;
                                break;
                            case 'W':
                                location[1] -= value;
                                break;
                        }
                        break;
                }
            }

            return location;
        }

        static char TurnLeft(char facing, int degrees)
        {
            char[] directions = new char[] { 'N', 'E', 'S', 'W' };

            while (degrees > 0)
            {
                if (facing == directions[0])
                {
                    facing = directions[^1];
                }
                else
                {
                    facing = directions[Array.IndexOf(directions, facing) - 1];
                }

                degrees -= 90;
            }

            return facing;
        }

        static char TurnRight(char facing, int degrees)
        {
            char[] directions = new char[] { 'N', 'E', 'S', 'W' };

            while (degrees > 0)
            {
                if (facing == directions[^1])
                {
                    facing = directions[0];
                }
                else
                {
                    facing = directions[Array.IndexOf(directions, facing) + 1];
                }

                degrees -= 90;
            }

            return facing;
        }
    }
}
