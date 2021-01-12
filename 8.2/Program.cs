using System;
using System.Collections.Generic;
using System.IO;

namespace _8._2
{
    class Program
    {
        struct BoolAndInt
        {
            public bool isTerminatingLoop;
            public int acc;
        }

        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");

            int[] instructionsJmpAndNop = ListIndexesJmpAndNop(input);
            
            Console.WriteLine("The value of the accumulator after the terminating loop is {0}.", GetAccumulator(input, instructionsJmpAndNop));
        }

        static int[] ListIndexesJmpAndNop (string[] input)
        {
            List<int> indexesJmpAndNop = new List<int>();

            for (int i = 0; i < input.Length; i++)
            {
                char operation = input[i][0];

                if (operation == 'j' || operation == 'n')
                {
                    indexesJmpAndNop.Add(i);
                }
            }

            return indexesJmpAndNop.ToArray();
        }

        static int GetAccumulator (string[] input, int[] indexesJmpAndNop)
        {
            int acc = 0;

            foreach (int index in indexesJmpAndNop)
            {
                string[] changedInput = new string[input.Length];

                Array.Copy(input, changedInput, input.Length);

                if (changedInput[index][0] == 'j')
                {
                    changedInput[index] = changedInput[index].Replace('j', 'n');
                }
                else if (changedInput[index][0] == 'n')
                {
                    changedInput[index] = changedInput[index].Replace('n', 'j');
                }

                BoolAndInt returnValue = FindTerminatingLoop(changedInput);

                if (returnValue.isTerminatingLoop)
                {
                    acc = returnValue.acc;
                    break;
                }
            }

            return acc;
        }

        static BoolAndInt FindTerminatingLoop (string[] changedInput)
        {
            BoolAndInt returnValue = new BoolAndInt
            {
                isTerminatingLoop = false,
                acc = 0
            };

            int index = 0;

            List<int> runInstructions = new List<int>();

            while (!runInstructions.Contains(index))
            {
                if (index > changedInput.Length - 1)
                {
                    returnValue.isTerminatingLoop = true;
                    return returnValue;
                }

                char operation = changedInput[index][0];

                int argument = int.Parse(changedInput[index].Substring(4));

                runInstructions.Add(index);

                if (operation == 'a')
                {
                    returnValue.acc += argument;
                    index++;
                }
                else if (operation == 'j')
                {
                    index += argument;
                }
                else if (operation == 'n')
                {
                    index++;
                }
            }

            return returnValue;
        }
    }
}
