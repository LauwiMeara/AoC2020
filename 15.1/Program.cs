using System;
using System.Collections.Generic;

namespace _15._1
{
    class Program
    {
        static void Main()
        {
            List<int> numbers = new List<int> { 7, 12, 1, 0, 16, 2 };

            int turn = numbers.Count + 1;

            while (turn <= 2020)
            {
                int lastNumber = numbers[turn - 2];
                int age;
                int index;

                for (index = numbers.Count - 2; index >= 0; index--)
                {
                    if (numbers[index] == lastNumber)
                    {
                        age = (turn - 2) - index;
                        numbers.Add(age);
                        break;
                    }
                }

                if (index == -1)
                {
                    age = 0;
                    numbers.Add(age);
                }

                turn++;
            }

            Console.WriteLine($"The 2020th number is {numbers[^1]}.");
        }
    }
}
