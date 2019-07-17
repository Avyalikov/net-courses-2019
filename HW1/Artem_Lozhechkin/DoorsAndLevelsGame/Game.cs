using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This class represents a Doors and Levels game.
    /// </summary>
    class Game
    {
        /// <summary>
        /// This array of longs represents a doors at every level. Long is used to handle OverflowException which raises too often because of multiplying a number by itself, but it still raises at levels 5-6.
        /// </summary>
        private long[] Numbers { get; } = new long[5];
        /// <summary>
        /// This is a stack that saves all chosen numbers.
        /// </summary>
        private Stack<int> ChosenNumbers { get; } = new Stack<int>();
        /// <summary>
        /// Random for filling Numbers array.
        /// </summary>
        private readonly Random random = new Random();
        /// <summary>
        /// Constructor which initializes Numbers array.
        /// </summary>
        public Game()
        {
            for (int i = 0; i < Numbers.Length-1; i++)
            {
                Numbers[i] = random.Next(1, 10);
            }

            Numbers[Numbers.Length-1] = 0;
        }

        /// <summary>
        /// This is a method used to start the game.
        /// </summary>
        public void Play()
        {
            Console.WriteLine("Welcome to The Doors and Levels game!");
            while (true)
            {
                Console.Write($"\nLevel {ChosenNumbers.Count + 1}\nWe have numbers: ");
                PrintNumbers();
                Console.WriteLine();
                GetNumberFromPlayer();
                Proceed();
            }
        }

        /// <summary>
        /// This method gets the correct number from user and inserts it at the top of the ChosenNumbersStack. 
        /// </summary>
        private void GetNumberFromPlayer()
        {
            while (true)
            {
                Console.Write("Select your number: ");
                int choice;
                // try-catch is used to detect incorrect choice
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("You should choose a correct number. Try again.");
                    continue;
                }
                if (Numbers.Contains(choice))
                {
                    ChosenNumbers.Push(choice);
                    break;
                }
                else Console.WriteLine("You should choose one of the above numbers. Try again.");
            }
        }
        /// <summary>
        /// This method proceeds to the next or previous level.
        /// </summary>
        private void Proceed()
        {
            int choice = ChosenNumbers.Peek();
            // If choice is 0, then we should go back to the previous level or stay if we are at level 0.
            if (choice == 0)
            {
                if (ChosenNumbers.Count > 1)
                {
                    Console.Write($"You сhose {choice} and went to previous level: ");
                    ChosenNumbers.Pop();
                    int previousChoice = ChosenNumbers.Pop();
                    for (int i = 0; i < Numbers.Length; i++)
                    {
                        Numbers[i] /= previousChoice;
                    }
                    PrintNumbers();
                    Console.WriteLine();
                }
                else
                {
                    ChosenNumbers.Pop();
                    Console.WriteLine("You cannot go back, because you are at level 1");
                }
            }
            // If choice is not 0, then we should go to the next level.
            else
            {
                Console.Write($"You сhose {choice} and went to the next level: ");
                StringBuilder levelLogs = new StringBuilder("( ");

                for (int i = 0; i < Numbers.Length; i++)
                {
                    levelLogs.AppendFormat($"{Numbers[i]}x{choice} ");
                    Numbers[i] *= choice;
                }

                levelLogs.Append(")");
                PrintNumbers();
                Console.WriteLine(levelLogs);
            }
        }
        /// <summary>
        /// This method puts a Numbers array in output
        /// </summary>
        private void PrintNumbers()
        {
            foreach (long number in Numbers)
            {
                Console.Write(number + " ");
            }
        }
    }
}
