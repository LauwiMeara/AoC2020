using System;
using System.IO;

namespace _12._2
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");

            int[] locationShip = GetNewLocationShip(input);

            int manhattanDistance = Math.Abs(locationShip[0]) + Math.Abs(locationShip[1]);
            Console.WriteLine($"The Manhattan distance is {manhattanDistance}.");
        }

        static int[] GetNewLocationShip(string[] input)
        {
            int[] locationShip = new int[] { 0, 0 };
            int[] locationWaypoint = new int[] { 1, 10 };

            foreach (string instruction in input)
            {
                char action = instruction[0];
                int value = int.Parse(instruction.Substring(1));

                switch (action)
                {
                    case 'N':
                        locationWaypoint[0] += value;
                        break;
                    case 'S':
                        locationWaypoint[0] -= value;
                        break;
                    case 'E':
                        locationWaypoint[1] += value;
                        break;
                    case 'W':
                        locationWaypoint[1] -= value;
                        break;
                    case 'L':
                        while (value > 0)
                        {
                            int tempLocation = locationWaypoint[0];
                            locationWaypoint[0] = locationWaypoint[1];
                            locationWaypoint[1] = -tempLocation;
                            value -= 90;
                        }
                        break;
                    case 'R':
                        while (value > 0)
                        {
                            int tempLocation = locationWaypoint[1];
                            locationWaypoint[1] = locationWaypoint[0];
                            locationWaypoint[0] = -tempLocation;
                            value -= 90;
                        }
                        break;
                    case 'F':
                        locationShip[0] += (locationWaypoint[0] * value);
                        locationShip[1] += (locationWaypoint[1] * value);
                        break;
                }
            }

            return locationShip;
        }
    }
}
