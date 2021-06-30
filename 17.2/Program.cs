using System;
using System.IO;

namespace _17._2
{
    class Program
    {
        static void Main()
        {
            int numCycles = 6;

            char[,,,] curCube = InitCube(numCycles);

            for (int i = 0; i < numCycles; i++)
            {
                char[,,,] newCube = TransformCube(curCube);
                curCube = newCube;
            }

            Console.WriteLine($"The number of active cubes is {CountActiveCubes(curCube)}");

            // PrintLayout(curCube);
        }

        static char[,,,] InitCube(int numCycles)
        {
            string[] input = File.ReadAllLines("input.txt");

            int initCubeLength = input.Length;
            int finalCubeLength = initCubeLength + (numCycles * 2);
            int startIndex = (finalCubeLength - initCubeLength) / 2;

            char[,,,] cube = new char[finalCubeLength, finalCubeLength, finalCubeLength, finalCubeLength];

            for (int x = 0; x < finalCubeLength; x++)
            {
                for (int y = 0; y < finalCubeLength; y++)
                {
                    for (int z = 0; z < finalCubeLength; z++)
                    {
                        for (int w = 0; w < finalCubeLength; w++)
                        {
                            if (x >= startIndex && x < startIndex + initCubeLength && y >= startIndex && y < startIndex + initCubeLength && z == finalCubeLength / 2 && w == finalCubeLength / 2)
                            {
                                cube[x, y, z, w] = input[x - numCycles][y - numCycles];
                            }
                            else
                            {
                                cube[x, y, z, w] = '.';
                            }
                        }
                    }
                }
            }

            return cube;
        }

        static char[,,,] TransformCube(char[,,,] curCube)
        {
            int cubeLength = curCube.GetLength(0);
            char[,,,] newCube = new char[cubeLength, cubeLength, cubeLength, cubeLength];

            for (int x = 0; x < cubeLength; x++)
            {
                for (int y = 0; y < cubeLength; y++)
                {
                    for (int z = 0; z < cubeLength; z++)
                    {
                        for (int w = 0; w < cubeLength; w++)
                        {
                            int numActiveNeighbours = CountActiveNeighbours(curCube, x, y, z, w);

                            if (curCube[x, y, z, w] == '#' && (numActiveNeighbours == 2 || numActiveNeighbours == 3))
                            {
                                newCube[x, y, z, w] = '#';
                            }
                            else if (curCube[x, y, z, w] == '.' && numActiveNeighbours == 3)
                            {
                                newCube[x, y, z, w] = '#';
                            }
                            else
                            {
                                newCube[x, y, z, w] = '.';
                            }
                        }
                    }
                }
            }

            return newCube;
        }

        static int CountActiveNeighbours(char[,,,] curCube, int x, int y, int z, int w)
        {
            int cubeLength = curCube.GetLength(0);
            int numActiveNeighbours = curCube[x, y, z, w] == '#' ? -1 : 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i == -1 || i == cubeLength) continue;

                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (j == -1 || j == cubeLength) continue;

                    for (int k = z - 1; k <= z + 1; k++)
                    {
                        if (k == -1 || k == cubeLength) continue;

                        for (int l = w - 1; l <= w + 1; l++)
                        {
                            if (l == -1 || l == cubeLength) continue;

                            if (curCube[i, j, k, l] == '#') numActiveNeighbours++;
                        }
                    }
                }
            }

            return numActiveNeighbours;
        }

        static int CountActiveCubes(char[,,,] curCube)
        {
            int cubeLength = curCube.GetLength(0);
            int numActiveCubes = 0;

            for (int x = 0; x < cubeLength; x++)
            {
                for (int y = 0; y < cubeLength; y++)
                {
                    for (int z = 0; z < cubeLength; z++)
                    {
                        for (int w = 0; w < cubeLength; w++)
                        {
                            if (curCube[x, y, z, w] == '#') numActiveCubes++;
                        }
                    }
                }
            }

            return numActiveCubes;
        }

        static void PrintLayout(char[,,,] curCube) {
            int cubeLength = curCube.GetLength(0);

            for (int w = 0; w < cubeLength; w++)
            {
                for (int z = 0; z < cubeLength; z++)
                {
                    Console.Write($"z = {z}, w = {w} {Environment.NewLine}");

                    for (int x = 0; x < cubeLength; x++)
                    {
                        for (int y = 0; y < cubeLength; y++)
                        {
                            Console.Write(curCube[x, y, z, w]);
                        }

                        Console.Write(Environment.NewLine);
                    }

                    Console.Write(Environment.NewLine);
                }
            }
        }
    }
}
