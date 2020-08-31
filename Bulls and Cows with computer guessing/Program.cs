using System;
using System.Collections.Generic;

namespace Bulls_and_Cows_with_computer_guessing
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = GetAllUniqueNumbers();
            string currentGuess;

            while (true)
            {
                currentGuess = GetNumber(numbers);

                if (!PromptBullsAndCows(out int bullsCount, out int cowsCount))
                {
                    continue;
                }

                if (bullsCount == 4)
                {
                    Console.WriteLine($"The number is {currentGuess}");
                    break;
                }

                GetResult(numbers, currentGuess, cowsCount, bullsCount);
                Console.WriteLine($"There are {numbers.Count} possible numbers remaining.");
            }
        }

        public static bool PromptBullsAndCows(out int bullsCount, out int cowsCount)
        {
            Console.Write("Please enter the number of bulls: ");
            string bulls = Console.ReadLine();
            if (!int.TryParse(bulls, out bullsCount))
            {
                cowsCount = 0;
                Console.WriteLine("Please enter digits!");
                return false;
            }

            Console.Write("Please enter the number of cows: ");
            string cows = Console.ReadLine();
            if (!int.TryParse(cows, out cowsCount))
            {
                Console.WriteLine("Please enter digits!");
                return false;
            }

            if (bullsCount < 0 || bullsCount > 4 || cowsCount < 0 || cowsCount > 4)
            {
                Console.WriteLine("Please enter a number between 0 and 4.");
                return false;
            }

            return true;
        }

        private static void GetResult(List<string> numbers, string currentGuess, int cowsCount, int bullsCount)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                if (cowsCount == 0 && bullsCount == 0)
                {
                    for (int j = 0; j < currentGuess.Length; j++)
                    {
                        char currentDigit = currentGuess[j];
                        numbers.RemoveAll(u => u.Contains(currentDigit));
                    }
                }
                else if (bullsCount == 0)
                {
                    for (int j = 0; j < currentGuess.Length; j++)
                    {
                        char currentDigit = currentGuess[j];

                        numbers.RemoveAll(u => u.IndexOf(currentDigit) == j);
                    }
                }
                else
                {
                    CountBullsAndCows(currentGuess, numbers[i], out int bulls, out int cows);
                    if (cows != cowsCount && bulls != bullsCount)
                    {
                        numbers.Remove(numbers[i]);
                        i--;
                    }
                }
            }

            numbers.Remove(currentGuess);
        }

        private static void CountBullsAndCows(string currentGuess, string currentNumber, out int bulls, out int cows)
        {
            bulls = 0;
            cows = 0;
            for (int j = 0; j < currentGuess.Length; j++)
            {
                char currentDigit = currentGuess[j];

                int index = currentNumber.IndexOf(currentDigit);
                if (index == j)
                {
                    bulls++;
                }
                else if (index >= 0)
                {
                    cows++;
                }
            }
        }

        private static string GetNumber(List<string> numbers)
        {
            var random = new Random().Next(numbers.Count);
            string currentGuess = numbers[random];
            Console.WriteLine(currentGuess);
            return currentGuess;
        }

        static List<string> GetAllUniqueNumbers()
        {
            List<string> numbers = new List<string>();
            for (int a = 1; a <= 9; a++)
            {
                for (int b = 1; b <= 9; b++)
                {
                    if (a == b)
                        continue;

                    for (int c = 1; c <= 9; c++)
                    {
                        if (a == c || b == c)
                            continue;
                        for (int d = 1; d <= 9; d++)
                        {
                            if (a == d || b == d || c == d)
                                continue;
                            string number = a.ToString() + b.ToString() + c.ToString() + d.ToString();
                            numbers.Add(number);
                        }
                    }
                }
            }

            return numbers;
        }
    }
}
