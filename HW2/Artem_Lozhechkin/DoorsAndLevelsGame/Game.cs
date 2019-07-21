﻿using System;
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
        /// This array of ints represents a doors at every level.
        /// </summary>
        private int[] Numbers { get; set; } = new int[5];
        /// <summary>
        /// This is an IArrayGenerator which gives a random array of ints.
        /// </summary>
        public IArrayGenerator<int> ArrayGenerator { get; }
        /// <summary>
        /// This is an IStackDataStorage that saves all chosen numbers.
        /// </summary>
        public IStackDataStorage<int> ChosenNumbers { get; }
        /// <summary>
        /// InputOutputDevice is used for interaction with user.
        /// </summary>
        public IInputOutputDevice InputOutputDevice { get; }
        /// <summary>
        /// SettingsProvider gives us a Settings information like door numbers or phrases.
        /// </summary>
        public ISettingsProvider SettingsProvider { get; }
        /// <summary>
        /// Constructor which initializes all game components.
        /// </summary>
        public Game(
            IArrayGenerator<int> arrayGenerator, 
            IStackDataStorage<int> stackDataStorage,
            IInputOutputDevice inputOutputDevice,
            ISettingsProvider settingsProvider)
        {
            ArrayGenerator = arrayGenerator;
            ChosenNumbers = stackDataStorage;
            InputOutputDevice = inputOutputDevice;
            SettingsProvider = settingsProvider;
        }

        /// <summary>
        /// This is a method used to start the game.
        /// </summary>
        public void Play()
        {
            InputOutputDevice.WriteLine(SettingsProvider.GetPhrase(PhraseTypes.Welcome));
            InputOutputDevice.WriteLine(SettingsProvider.GetPhrase(PhraseTypes.NumberOfDoors));

            Numbers = ArrayGenerator.GetArray(SettingsProvider.GetNumberOfDoors());

            while (ChosenNumbers.GetSize() < 4)
            {
                InputOutputDevice.WriteLine(SettingsProvider.GetPhrase(PhraseTypes.LevelMessage) + (ChosenNumbers.GetSize() + 1));
                InputOutputDevice.Write(SettingsProvider.GetPhrase(PhraseTypes.NumbersMessage));
                PrintNumbers();
                InputOutputDevice.WriteLine();
                GetNumberFromPlayer();
                Proceed();
            }
            InputOutputDevice.WriteLine();
            InputOutputDevice.Write(SettingsProvider.GetPhrase(PhraseTypes.WinMessage));
        }

        /// <summary>
        /// This method gets the correct number from user and inserts it at the top of the ChosenNumbers. 
        /// </summary>
        private void GetNumberFromPlayer()
        {
            while (true)
            {
                InputOutputDevice.Write(SettingsProvider.GetPhrase(PhraseTypes.SelectingNumber));
                int choice;
                // try-catch is used to detect incorrect choice
                try
                {
                    choice = int.Parse(InputOutputDevice.Read());
                }
                catch (Exception)
                {
                    InputOutputDevice.WriteError(SettingsProvider.GetPhrase(PhraseTypes.IncorrectChoice));
                    continue;
                }
                if (Numbers.Contains(choice))
                {
                    ChosenNumbers.Push(choice);
                    break;
                }
                else InputOutputDevice.WriteError(SettingsProvider.GetPhrase(PhraseTypes.IncorrectNumberChoice));
            }
        }
        /// <summary>
        /// This method proceeds to the next or to the previous level.
        /// </summary>
        private void Proceed()
        {
            int choice = ChosenNumbers.Peek();
            // If choice is 0, then we should go back to the previous level or stay if we are at level 0.
            if (choice == 0)
            {
                if (ChosenNumbers.GetSize() > 1)
                {
                    InputOutputDevice.Write(SettingsProvider.GetPhrase(PhraseTypes.LevellingDownMessage));
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
                    InputOutputDevice.WriteError(SettingsProvider.GetPhrase(PhraseTypes.LevellingDownErrorMessage));
                }
            }
            // If choice is not 0, then we should go to the next level. Return if level 4, because it is a last level.
            else
            {
                if (ChosenNumbers.GetSize() == 4) return;
                InputOutputDevice.Write(string.Format(SettingsProvider.GetPhrase(PhraseTypes.LevellingUpMessage), choice));

                StringBuilder levelLogs = new StringBuilder("( ");

                for (int i = 0; i < Numbers.Length; i++)
                {
                    levelLogs.AppendFormat($"{Numbers[i]}x{choice} ");
                    Numbers[i] *= choice;
                }

                levelLogs.Append(")");
                PrintNumbers();
                InputOutputDevice.WriteLine(levelLogs.ToString());
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
