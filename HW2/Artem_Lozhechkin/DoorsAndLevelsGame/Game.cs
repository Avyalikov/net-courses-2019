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
        private long[] Numbers { get; set; } = new long[5];
        /// <summary>
        /// This is a stack that saves all chosen numbers.
        /// </summary>
        public IArrayGenerator<long> ArrayGenerator { get; }
        public IStackDataStorage<int> ChosenNumbers { get; }
        public IInputOutputDevice InputOutputDevice { get; }

        /// <summary>
        /// Random for filling Numbers array.
        /// </summary>
        private readonly Random random = new Random();
        /// <summary>
        /// Constructor which initializes all game components.
        /// </summary>
        public Game(
            IArrayGenerator<long> arrayGenerator, 
            IStackDataStorage<int> stackDataStorage,
            IInputOutputDevice inputOutputDevice)
        {
            ArrayGenerator = arrayGenerator;
            ChosenNumbers = stackDataStorage;
            InputOutputDevice = inputOutputDevice;
        }

        /// <summary>
        /// This is a method used to start the game.
        /// </summary>
        public void Play()
        {
            InputOutputDevice.Write("Welcome to The Doors and Levels game!");

            Numbers = ArrayGenerator.GetArray(5);

            while (true)
            {
                InputOutputDevice.Write($"\nLevel {ChosenNumbers.GetSize() + 1}\nWe have numbers: ");
                PrintNumbers();
                InputOutputDevice.WriteLine();
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
                InputOutputDevice.Write("Select your number: ");
                int choice;
                // try-catch is used to detect incorrect choice
                try
                {
                    choice = int.Parse(InputOutputDevice.Read());
                }
                catch (Exception)
                {
                    InputOutputDevice.WriteError("You should choose a correct number. Try again.\n");
                    continue;
                }
                if (Numbers.Contains(choice))
                {
                    ChosenNumbers.Push(choice);
                    break;
                }
                else InputOutputDevice.WriteError("You should choose one of the above numbers. Try again.\n");
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
                if (ChosenNumbers.GetSize() > 1)
                {
                    InputOutputDevice.Write($"You сhose {choice} and went to previous level: ");
                    ChosenNumbers.Pop();
                    int previousChoice = ChosenNumbers.Pop();
                    for (int i = 0; i < Numbers.Length; i++)
                    {
                        Numbers[i] /= previousChoice;
                    }
                    PrintNumbers();
                    InputOutputDevice.WriteLine();
                }
                else
                {
                    ChosenNumbers.Pop();
                    InputOutputDevice.WriteError("You cannot go back, because you are at level 1");
                }
            }
            // If choice is not 0, then we should go to the next level.
            else
            {
                InputOutputDevice.Write($"You сhose {choice} and went to the next level: ");
                StringBuilder levelLogs = new StringBuilder("( ");

                for (int i = 0; i < Numbers.Length; i++)
                {
                    levelLogs.AppendFormat($"{Numbers[i]}x{choice} ");
                    Numbers[i] *= choice;
                }

                levelLogs.Append(")");
                PrintNumbers();
                InputOutputDevice.Write(levelLogs.ToString()+"\n");
            }
        }
        /// <summary>
        /// This method puts a Numbers array in output
        /// </summary>
        private void PrintNumbers()
        {
            foreach (long number in Numbers)
            {
                InputOutputDevice.Write(number + " ");
            }
        }
    }
}
