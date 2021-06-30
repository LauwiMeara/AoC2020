using System;
using System.IO;

namespace _17._1
{
    class Program
    {
        static void Main()
        {
            int numCycles = 6;

            char[,,] curCube = InitCube(numCycles);

            for (int i = 0; i < numCycles; i++)
            {
                char[,,] newCube = TransformCube(curCube);
                curCube = newCube;
            }

            Console.WriteLine($"The number of active cubes is {CountActiveCubes(curCube)}");

            PrintLayout(curCube);
        }

        static char[,,] InitCube(int numCycles)
        {
            string[] input = File.ReadAllLines("input.txt");

            int initCubeLength = input.Length;
            int finalCubeLength = initCubeLength + (numCycles * 2);
            int startIndex = (finalCubeLength - initCubeLength) / 2;

            char[,,] cube = new char[finalCubeLength, finalCubeLength, finalCubeLength];

            for (int x = 0; x < finalCubeLength; x++)
            {
                for (int y = 0; y < finalCubeLength; y++)
                {
                    for (int z = 0; z < finalCubeLength; z++)
                    {
                        if (x >= startIndex && x < startIndex + initCubeLength && y >= startIndex && y < startIndex + initCubeLength && z == finalCubeLength / 2)
                        {
                            cube[x, y, z] = input[x - numCycles][y - numCycles];
                        }
                        else
                        {
                            cube[x, y, z] = '.';
                        }
                    }
                }
            }

            return cube;
        }

        static char[,,] TransformCube(char[,,] curCube)
        {
            int cubeLength = curCube.GetLength(0);
            char[,,] newCube = new char[cubeLength, cubeLength, cubeLength];

            for (int x = 0; x < cubeLength; x++)
            {
                for (int y = 0; y < cubeLength; y++)
                {
                    for (int z = 0; z < cubeLength; z++)
                    {
                        int numActiveNeighbours = CountActiveNeighbours(curCube, x, y, z);

                        if (curCube[x, y, z] == '#' && (numActiveNeighbours == 2 || numActiveNeighbours == 3))
                        {
                            newCube[x, y, z] = '#';
                        }
                        else if (curCube[x, y, z] == '.' && numActiveNeighbours == 3)
                        {
                            newCube[x, y, z] = '#';
                        }
                        else
                        {
                            newCube[x, y, z] = '.';
                        }
                    }
                }
            }

            return newCube;
        }

        static int CountActiveNeighbours(char[,,] curCube, int x, int y, int z)
        {
            int cubeLength = curCube.GetLength(0);
            int numActiveNeighbours = curCube[x, y, z] == '#' ? -1 : 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i == -1 || i == cubeLength) continue;

                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (j == -1 || j == cubeLength) continue;

                    for (int k = z - 1; k <= z + 1; k++)
                    {
                        if (k == -1 || k == cubeLength) continue;

                        if (curCube[i, j, k] == '#') numActiveNeighbours++;
                    }
                }
            }

            return numActiveNeighbours;
        }

        static int CountActiveCubes(char[,,] curCube)
        {
            int cubeLength = curCube.GetLength(0);
            int numActiveCubes = 0;

            for (int x = 0; x < cubeLength; x++)
            {
                for (int y = 0; y < cubeLength; y++)
                {
                    for (int z = 0; z < cubeLength; z++)
                    {
                        if (curCube[x, y, z] == '#') numActiveCubes++;
                    }
                }
            }

            return numActiveCubes;
        }

        static void PrintLayout(char[,,] curCube) {
            int cubeLength = curCube.GetLength(0);

            for (int z = 0; z < cubeLength; z++)
            {
                Console.Write($"z = {z} {Environment.NewLine}");

                for (int x = 0; x < cubeLength; x++)
                {
                    for (int y = 0; y < cubeLength; y++)
                    {
                        Console.Write(curCube[x, y, z]);
                    }

                    Console.Write(Environment.NewLine);
                }

                Console.Write(Environment.NewLine);
            }
        }
    }
}
