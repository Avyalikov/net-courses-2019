using System;
using System.Collections.Generic;

namespace CreateDoorsAndLevels
{
    class Game
    {
        private readonly Interfaces.IPhraseProvider phraseProvider;
        private readonly Interfaces.IInputOutputDevice inputOutputDevice;
        private readonly Interfaces.IDoorsNumbersGenerator doorsNumbersGenerator;
        private readonly Interfaces.ISettingsProvider settingsProvider;

        private readonly GameSettings gameSettings;

        private bool hardExit;

        List<int> numbers;
        List<int> selectedNumbers;

        public Game(
            Interfaces.IPhraseProvider phraseProvider, 
            Interfaces.IInputOutputDevice inputOutputDevice, 
            Interfaces.IDoorsNumbersGenerator doorsNumbersGenerator,
            Interfaces.ISettingsProvider settingsProvider
            )
        {
            this.phraseProvider = phraseProvider;
            this.inputOutputDevice = inputOutputDevice;
            this.doorsNumbersGenerator = doorsNumbersGenerator;
            this.settingsProvider = settingsProvider;

            this.gameSettings = this.settingsProvider.GetGameSettings();
        }

        public void Run()
        {
            phraseProvider.SetLanguage(gameSettings.Language);
            this.numbers = doorsNumbersGenerator.generateDoorsNumbers(gameSettings.DoorsAmount);
            this.selectedNumbers = new List<int>();

            inputOutputDevice.WriteLineOutput(phraseProvider.GetPhrase("Welcome"));

            // inputOutputDevice.WriteOutput($"We have numbers: 2, 4, 3, 1, 0");
            inputOutputDevice.WriteLineOutput(StartLevel());

            do
            {
                this.selectedNumbers.Add(EnterTheNumber()); // Select one of the current level numbers or type 'exit'

                if (this.selectedNumbers[this.selectedNumbers.Count - 1] == -1) break; // if -1 then break the circle - EXIT

                if (this.selectedNumbers[this.selectedNumbers.Count - 1] != this.gameSettings.ExitDoorNumber)
                {
                    inputOutputDevice.WriteLineOutput(NextLevel(this.selectedNumbers[this.selectedNumbers.Count - 1])); // "We select number 2 and go to next level: 4 8 6 2 0 (2x2 4x2 3x2 1x2 0x2)"
                    if (hardExit)
                    {
                        inputOutputDevice.WriteLineOutput(phraseProvider.GetPhrase("OverflowDoor"));
                        break;
                    }
                }
                else if (this.selectedNumbers.Count - 1 > 0) // selectedNumber == this.gameSettings.ExitDoorNumber, level > 0
                {
                    this.selectedNumbers.RemoveAt(this.selectedNumbers.Count - 1); // remove ExitDoorNumber from selectedNumbers. Now top is prev.number
                    inputOutputDevice.WriteLineOutput(PrevLevel()); // "We select number 0 and go to previous level: 4 8 6 2 0"
                    this.selectedNumbers.RemoveAt(this.selectedNumbers.Count - 1); // remove prev.number from selectedNumbers. Now top is prev2.number
                }
            } while (!(this.selectedNumbers.Count - 1 == 0 && this.selectedNumbers[0] == this.gameSettings.ExitDoorNumber));
            inputOutputDevice.WriteLineOutput(phraseProvider.GetPhrase("Bye"));
        }

        private int EnterTheNumber()
        {
            int result = -1;

            do
            {
                if (this.selectedNumbers.Count <= this.gameSettings.LevelLimit)
                {
                    inputOutputDevice.WriteOutput($"{phraseProvider.GetPhrase("Select")}{gameSettings.ExitCode}{phraseProvider.GetPhrase("AfterExitCode")}");
                }
                else
                {
                    inputOutputDevice.WriteOutput($"{phraseProvider.GetPhrase("SelectExitDoor")}{gameSettings.ExitDoorNumber}{phraseProvider.GetPhrase("ToContinue")}{gameSettings.ExitCode}{phraseProvider.GetPhrase("AfterExitCode")}");
                }

                try
                {
                    string stringNumber = inputOutputDevice.ReadInput();

                    if (String.IsNullOrEmpty(stringNumber))
                    {
                        inputOutputDevice.WriteLineOutput(phraseProvider.GetPhrase("EmptyString"));
                        continue;
                    }

                    if (stringNumber.ToLowerInvariant().Equals(gameSettings.ExitCode.ToLowerInvariant())) break; // if 'exit' then break the circle. result will be -1

                    int intNumber = Int32.Parse(stringNumber);

                    if (!this.numbers.Contains(intNumber))
                    {
                        inputOutputDevice.WriteLineOutput(phraseProvider.GetPhrase("WrongNumber"));
                        continue;
                    }

                    if (this.selectedNumbers.Count > this.gameSettings.LevelLimit && intNumber != this.gameSettings.ExitDoorNumber)
                    {
                        inputOutputDevice.WriteLineOutput($"{phraseProvider.GetPhrase("NotExitDoor")}{gameSettings.ExitDoorNumber}");
                        continue;
                    }

                    result = intNumber;
                }
                catch (OverflowException)
                {
                    inputOutputDevice.WriteLineOutput(phraseProvider.GetPhrase("Overflow"));
                    continue;
                }
                catch
                {
                    inputOutputDevice.WriteLineOutput(phraseProvider.GetPhrase("InputError"));
                    continue;
                }
            } while (result == -1);

            return result;
        }

        private string StartLevel()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(phraseProvider.GetPhrase("YouHave"));

            for (int i = 0; i < numbers.Count; i++)
            {
                sb.Append(numbers[i]);
                sb.Append(i < numbers.Count - 1 ? " " : String.Empty); // add space between them
            }

            return sb.ToString();
        }

        private string NextLevel(int selectedNumber)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(phraseProvider.GetPhrase("YouSelected")).Append(selectedNumber).Append(phraseProvider.GetPhrase("GoNext"));

            for (int i = 0; i < numbers.Count; i++)
            {
                sb.Append(numbers[i] *= selectedNumber);
                if (numbers[i] < 0)
                {
                    hardExit = true;
                    return "";
                }
                sb.Append(i < numbers.Count - 1 ? " " : String.Empty); // add space between them;
            }

            sb.Append(" (");
            for (int i = 0; i < numbers.Count; i++)
            {
                sb.Append($"{numbers[i] / selectedNumber}x{selectedNumber}");
                sb.Append(i < numbers.Count - 1 ? " " : String.Empty); // add space between them
            }
            sb.Append(")");

            return sb.ToString();
        }

        private string PrevLevel()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(phraseProvider.GetPhrase("YouSelected")).Append(gameSettings.ExitDoorNumber).Append(phraseProvider.GetPhrase("GoPrev"));

            for (int i = 0; i < numbers.Count; i++)
            {
                sb.Append(numbers[i] /= this.selectedNumbers[this.selectedNumbers.Count - 1]);
                sb.Append(i < numbers.Count - 1 ? " " : String.Empty); // add space between them;
            }

            return sb.ToString();
        }
    }
}
