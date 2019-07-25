using System;
using System.Collections.Generic;

namespace HW2_doors_and_levels_refactoring
{
    public class Game
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutputDevice ioDevice;
        private readonly IStartNumbersGenerator startNumbersGenerator; 
        private readonly INumbersChanger numbersChanger;
        private readonly GameSettings gameSettings;
        private int[] NumbersArray;
        private Stack<int> usernums;


        public Game(
            IPhraseProvider phraseProvider, 
            IInputOutputDevice ioDevice, 
            IStartNumbersGenerator startNumbersGenerator,
            INumbersChanger numbersChanger,
            ISettingsProvider gameSettings)
        {
            this.phraseProvider = phraseProvider;
            this.ioDevice = ioDevice;
            this.startNumbersGenerator = startNumbersGenerator;
            this.numbersChanger = numbersChanger;
            this.gameSettings = gameSettings.GetGameSettings();
        }

        public void Run()
        {
            NumbersArray = startNumbersGenerator.GenerateStartNumbers(gameSettings.DoorsAmount); // array with the current numbers
            usernums = new Stack<int>();
            string UserInput; // user input through io device
            string PreviousLevel = phraseProvider.GetPhrase("PreviousLevelNumber") + gameSettings.PreviousLevelNumber;
            string ExitCode = phraseProvider.GetPhrase("ExitCode") + gameSettings.ExitCode;
            ioDevice.Print(phraseProvider.GetPhrase("Welcome"));
            ioDevice.Print(PreviousLevel);
            ioDevice.Print(ExitCode);

            while (true)
            {
                string Numbers = phraseProvider.GetPhrase("TheNumbersAre");
                foreach (int number in NumbersArray)
                {
                    Numbers = Numbers + number + " ";
                }
                Numbers += phraseProvider.GetPhrase("SelectAndEnterNumber");
                ioDevice.Print(Numbers);
                UserInput = ioDevice.InputValue();
                if (UserInput.ToLowerInvariant() == gameSettings.ExitCode.ToLowerInvariant()) break; // exit command
                NumbersArray = numbersChanger.ChangeNumbers(NumbersArray, UserInput, usernums);
            }
            ioDevice.Print(phraseProvider.GetPhrase("ThankYouForPlaying"));
        }
    }
}
