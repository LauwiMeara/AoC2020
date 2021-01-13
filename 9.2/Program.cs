using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _9._2
{
    class Program
    {
        struct IntAndLong
        {
            public int IndexOfNum;
            public long Num;
        }

        static void Main()
        {
            long[] input = File.ReadAllLines("input.txt").Select(long.Parse).ToArray();

            List<long> preamble = new List<long>();

            const int preambleLength = 25;

            for (int i = 0; i < preambleLength; i++)
            {
                preamble.Add(input[i]);
            }

            IntAndLong numWithoutPreambleProperty = GetNumWithoutPreambleProperty(preambleLength, input, preamble);

            Console.WriteLine("The encryption weakness is {0}.", GetEncryptionWeakness(input, numWithoutPreambleProperty));
        }

        static IntAndLong GetNumWithoutPreambleProperty(int preambleLength, long[] input, List<long> preamble)
        {
            IntAndLong numWithoutPreambleProperty = new IntAndLong();

            for (int i = preambleLength; i < input.Length; i++)
            {
                for (int j = preamble.Count - 1; j > 0; j--)
                {
                    long remainder = input[i] - preamble[j];
                    int index = preamble.IndexOf(remainder);

                    if (index == j)
                    {
                        break;
                    }
                    else if (preamble.IndexOf(remainder) != -1)
                    {
                        preamble.Add(input[i]);
                        preamble.RemoveAt(0);
                        break;
                    }
                }

                if (preamble.IndexOf(input[i]) == -1)
                {
                    numWithoutPreambleProperty.IndexOfNum = i;
                    numWithoutPreambleProperty.Num = input[i];
                    break;
                }
            }

            return numWithoutPreambleProperty;
        }

        static long GetEncryptionWeakness(long[] input, IntAndLong numWithoutPreambleProperty)
        {
            List<long> inputUntilNum = new List<long>();

            long encryptionWeakness = 0;

            for (int i = 0; i < numWithoutPreambleProperty.IndexOfNum - 1; i++)
            {
                inputUntilNum.Add(input[i]);
                
                long sum = input[i];

                for (int j = i + 1; j < numWithoutPreambleProperty.IndexOfNum; j++)
                {
                    inputUntilNum.Add(input[j]);
                    sum += input[j];

                    if (sum == numWithoutPreambleProperty.Num)
                    {
                        inputUntilNum.Sort();
                        encryptionWeakness = inputUntilNum[0] + inputUntilNum[inputUntilNum.Count - 1];
                        break;
                    }
                    else if (sum > numWithoutPreambleProperty.Num)
                    {
                        break;
                    }
                }

                inputUntilNum.Clear();
            }

            return encryptionWeakness;
        }
    }
}
