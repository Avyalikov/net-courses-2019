using DoorsAndLevelsGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorsAndLevelsGame
{
    class Game
    {
        private readonly IDoorsNumbersGenerator doorsNumbersGenerator;
        private readonly IInputOutputDevice inputOutputDevice;
        private readonly IPhraseProvider phraseProvider;
        private readonly ISettingsProvider settingsProvider;
        private GameSettings gameSettings;

        public int[] doorNumbersArray;
        public Stack<int> levelDoorNumberStack = new Stack<int>();
        public int userDoorSelect;

        public Game(
            IDoorsNumbersGenerator doorsNumbersGenerator,
            IInputOutputDevice inputOutputDevice,
            IPhraseProvider phraseProvider,
            ISettingsProvider settingsProvider,
            GameSettings gameSettings)
        {
            this.doorsNumbersGenerator = doorsNumbersGenerator;
            this.inputOutputDevice = inputOutputDevice;
            this.phraseProvider = phraseProvider;
            this.settingsProvider = settingsProvider;
            this.gameSettings = gameSettings;
            doorNumbersArray = doorsNumbersGenerator.GenerateDoorsNumbers(settingsProvider.GetGameSettings().DoorsAmount);
        }

        public void Run()
        {
            inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("Start"));
            inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("Select-0") + settingsProvider.GetGameSettings().ExitDoorNumber);
            inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("Select-1") + settingsProvider.GetGameSettings().ExitCode);

            inputOutputDevice.WriteOutputArray(doorNumbersArray);

            while (true)
            {
                inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("SelectNumber"));

                string userNumbrInput = inputOutputDevice.ReadInput();
                int tryParse;

                if (int.TryParse(userNumbrInput, out tryParse))
                {
                    userDoorSelect = tryParse;
                    if (userDoorSelect == settingsProvider.GetGameSettings().ExitCode)
                    {
                        break;
                    }

                    CheckLine(userDoorSelect);

                    inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("#"));

                    inputOutputDevice.WriteOutputArray(doorNumbersArray);
                }
                else
                {
                    inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("WrongValue"));
                }
            }
        }

        private void CheckLine(int userDoorSelect)
        {
            if (doorNumbersArray.Contains(userDoorSelect))
            {
                if (userDoorSelect != 0)
                {
                    levelDoorNumberStack.Push(userDoorSelect);
                    doorNumbersArray = LevelUp(doorNumbersArray, userDoorSelect);
                }

                if (userDoorSelect == 0)
                {
                    if (levelDoorNumberStack.Count > 0)
                    {
                        LevelDown(doorNumbersArray, levelDoorNumberStack.Pop());
                    }
                    else
                    {
                        inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("StackIsEmpty"));                       
                    }           
                }
            }
            else
            {
                inputOutputDevice.WriteOutput(phraseProvider.GetPhrase("WrongValue"));
            }
        }

        private int[] LevelUp(int[] doorNumbersArray, int userDoorSelect)
        {

            for (int i = 0; i < doorNumbersArray.Length; i++)
            {
                doorNumbersArray[i] *= userDoorSelect;
            }
            return doorNumbersArray;
        }

        private int[] LevelDown(int[] doorNumbersArray, int userDoorSelect)
        {
            for (int i = 0; i < doorNumbersArray.Length; i++)
            {
                doorNumbersArray[i] /= userDoorSelect;
            }
            return doorNumbersArray;
        }
    }
}
