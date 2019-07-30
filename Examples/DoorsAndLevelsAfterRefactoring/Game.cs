using System;
using System.Collections.Generic;

namespace DoorsAndLevelsAfterRefactoring
{
    public class Game : IGame
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutputDevice ioDevice;
        private readonly ISettingsProvider settingsProvider;
        private readonly IDoorsNumbersGenerator doorsNumbersGenerator;

        private readonly GameSettings gameSettings;

        private int[] NumbersArray;
        private List<int> UserNumbers;

        public Game(
            IPhraseProvider phraseProvider, 
            IInputOutputDevice ioDevice, 
            GameSettings gameSettings, 
            IDoorsNumbersGenerator doorsNumbersGenerator)
        {
            this.phraseProvider = phraseProvider;
            this.ioDevice = ioDevice;
            this.gameSettings = gameSettings;
            this.doorsNumbersGenerator = doorsNumbersGenerator;
        }

        public void Run()
        {
            NumbersArray = doorsNumbersGenerator.GenerateDoorsNumbers(gameSettings.DoorsAmount); // array with the current numbers
            UserNumbers = new List<int> { 1 }; // numbers entered by user from the start except 0
            string UserInput; // user input through io device

            ioDevice.WriteOutput(phraseProvider.GetPhrase("Welcome"));

            while (true)
            {
                string Numbers = phraseProvider.GetPhrase("TheNumbersAre");
                foreach (int number in NumbersArray)
                {
                    Numbers = Numbers + number + " ";
                }
                Numbers += phraseProvider.GetPhrase("SelectAndEnterNumber");
                ioDevice.WriteOutput(Numbers);
                UserInput = ioDevice.ReadInput();
                if (UserInput.ToLowerInvariant() == gameSettings.ExitCode.ToLowerInvariant()) break; // exit command
                NumbersChanger(UserInput);
            }
            ioDevice.WriteOutput(phraseProvider.GetPhrase("ThankYouForPlaying"));
            ioDevice.ReadKey();
        }

        //validates the input and changes the numbers in array
        private void NumbersChanger(string UserInput)
        {
            int Temp;
            // validates entered string is a number
            if (!Int32.TryParse(UserInput, out Temp))
            {
                ioDevice.WriteOutput(phraseProvider.GetPhrase("YouHaveEnteredWrongValue"));
                return;
            }
            // goint to previous level
            if (Temp == gameSettings.ExitDoorNumber)
            {
                for (int i = 0; i < NumbersArray.Length; i++)
                {
                    if (NumbersArray[i] != gameSettings.ExitDoorNumber)
                    {
                        NumbersArray[i] /= UserNumbers[UserNumbers.Count - 1];
                    }
                }
                if (UserNumbers.Count > 1) UserNumbers.RemoveAt(UserNumbers.Count - 1);
                return;
            }
            //validating entered number
            foreach (int number in NumbersArray)
            {
                //if value contains in the array -> multiply array numbers
                if (number == Temp)
                {
                    for (int i = 0; i < NumbersArray.Length; i++)
                    {
                        if (NumbersArray[i] != gameSettings.ExitDoorNumber)
                        {
                            NumbersArray[i] *= Temp;
                        }
                    }
                    UserNumbers.Add(Temp); // adding value to the List
                    return;
                }
            }
            //if array doesn't contain entered number
            ioDevice.WriteOutput(phraseProvider.GetPhrase("YouHaveEnteredWrongValuePleaseEnterNumbersListed"));
            return;
        }
    }
}
