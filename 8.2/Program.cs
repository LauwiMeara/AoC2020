using System;
using System.Collections.Generic;
using System.IO;

namespace _8._2
{
    class Program
    {
        struct BoolAndInt
        {
            public bool IsTerminatingLoop;
            public int Acc;
        }

        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");

            int[] indexesJmpAndNop = GetIndexesJmpAndNop(input);

            Console.WriteLine("The value of the accumulator after the terminating loop is {0}.", GetAccumulator(input, indexesJmpAndNop));
        }

        static int[] GetIndexesJmpAndNop (string[] input)
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

                string operation = changedInput[index].Split(" ")[0];

                if (operation == "jmp")
                {
                    changedInput[index] = changedInput[index].Replace("jmp", "nop");
                }
                else if (operation == "nop")
                {
                    changedInput[index] = changedInput[index].Replace("nop", "jmp");
                }

                BoolAndInt loopState = GetLoopState(changedInput);

                if (loopState.IsTerminatingLoop)
                {
                    acc = loopState.Acc;
                    break;
                }
            }

            return acc;
        }

        static BoolAndInt GetLoopState (string[] changedInput)
        {
            BoolAndInt loopState = new BoolAndInt();

            int index = 0;

            List<int> runInstructions = new List<int>();

            while (!runInstructions.Contains(index))
            {
                if (index > changedInput.Length - 1)
                {
                    loopState.IsTerminatingLoop = true;
                    return loopState;
                }

                string[] instructionParts = changedInput[index].Split(' ');

                string operation = instructionParts[0];

                int argument = int.Parse(instructionParts[1]);

                runInstructions.Add(index);

                if (operation == "acc")
                {
                    loopState.Acc += argument;
                    index++;
                }
                else if (operation == "jmp")
                {
                    index += argument;
                }
                else if (operation == "nop")
                {
                    index++;
                }
            }

            return loopState;
        }
    }
}
