using System;
using System.Collections.Generic;
using System.Linq;
using NumbersGame.Interfaces;

namespace NumbersGame
{   public enum Language {Eng, Rus}
    public enum InputCheckResult {Valid, Invalid, Exit, Info}

    public class Game
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutput ioModule;        
        private readonly IDoorsNumbersGenerator doorsNumbersGenerator;

        private readonly GameSettings gameSettings;
        private readonly Language ChosenLang;
                
        private int[] doorNumbersHolder;
        private Stack<int> userInputHolder;

        public Game
            (IPhraseProvider phraseProvider, IInputOutput ioModule, 
            ISettingsProvider settingsProvider, IDoorsNumbersGenerator doorsNumbersGenerator)
        {
            this.phraseProvider = phraseProvider;
            this.ioModule = ioModule;

            this.gameSettings = settingsProvider.GetGameSettings();
            this.doorNumbersHolder = doorsNumbersGenerator.GenerateDoorsNumbers(this.gameSettings.DoorsAmount);
            this.ChosenLang = SelectLang(this.phraseProvider, this.ioModule);
            this.userInputHolder = new Stack<int>();
        }

        private Language SelectLang(IPhraseProvider phraseProvider, IInputOutput ioModule)
        {
            while (true)
            {
                ioModule.WriteOutput(phraseProvider.GetPhrase("SelectLang", Language.Eng));
                string strInput = ioModule.ReadInput();
                if (string.IsNullOrEmpty(strInput)) continue;
                int numInput = Convert.ToInt32(strInput);
                if (numInput == 1) return Language.Eng;
                if (numInput == 2) return Language.Rus;
            }     
        }

        private InputCheckResult CheckIfInputIsValid(string input)
        {
            if (input == "q") return InputCheckResult.Exit;
            if (input == "i") return InputCheckResult.Info;
            try
            {
                int numInput = Convert.ToInt32(input);
                foreach (int number in this.doorNumbersHolder)
                {
                    if (numInput == number) return InputCheckResult.Valid;
                }
                return InputCheckResult.Invalid;
            }
            catch (System.FormatException)
            {
                return InputCheckResult.Invalid;
            }             
        }

        private bool CheckWinCondition()
        {
            foreach (int number in this.doorNumbersHolder)
            {
                if (number >= gameSettings.WinNumber) return true;
            }
            return false;
        }

        private void ShowInfo()
        {
            ioModule.WriteOutput(phraseProvider.GetPhrase("Info", ChosenLang));
            ioModule.WriteOutput(phraseProvider.GetPhrase("WinValueIs", ChosenLang));
            ioModule.WriteOutput(gameSettings.WinNumber.ToString());
            ioModule.WriteOutput(phraseProvider.GetPhrase("ExitKey", ChosenLang));
            ioModule.WriteOutput(gameSettings.ExitButton);
            ioModule.WriteOutput(phraseProvider.GetPhrase("InfoKey", ChosenLang));
            ioModule.WriteOutput(gameSettings.InfoButton);
        }


        public void Run()
        {
            bool exit = false;
            string userInput;
            
            InputCheckResult checkResult;
            ioModule.WriteOutput(phraseProvider.GetPhrase("Intro", ChosenLang));
            ShowInfo();

            while (!CheckWinCondition() && !exit)
            {
                string enterThisNum = phraseProvider.GetPhrase("EnterNumber", ChosenLang);
                foreach (int number in doorNumbersHolder)
                {
                    enterThisNum = enterThisNum + number + " ";
                }
                enterThisNum = enterThisNum + Environment.NewLine;
                ioModule.WriteOutput(enterThisNum);

                userInput = ioModule.ReadInput();
                if (string.IsNullOrEmpty(userInput))
                {
                    ioModule.WriteOutput(phraseProvider.GetPhrase("InvalidInput", ChosenLang)); continue;
                }
                checkResult = CheckIfInputIsValid(userInput);

                switch (checkResult)
                {
                    case InputCheckResult.Invalid :
                        {
                            ioModule.WriteOutput(phraseProvider.GetPhrase("InvalidInput", ChosenLang)); continue;
                        }

                    case InputCheckResult.Exit : { exit = true; break; }

                    case InputCheckResult.Info : { ShowInfo(); break; }

                    case InputCheckResult.Valid :
                        {
                            NumbersChanger(userInput);
                            break;
                        }
                }
            }
            
            if (CheckWinCondition())
            {
                ioModule.WriteOutput(phraseProvider.GetPhrase("Win", ChosenLang));                
            }

            ioModule.WriteOutput(phraseProvider.GetPhrase("Goodbye", ChosenLang));
        }

        private void NumbersChanger(string UserInput)
        {
            int userInputNum = Convert.ToInt32(UserInput);

            if (userInputNum == 0)
            {
                if (!userInputHolder.Any())
                {
                    ioModule.WriteOutput(phraseProvider.GetPhrase("FirstLvl", ChosenLang));
                    return;
                }

                int prevInput = userInputHolder.Pop();
                for (int i = 1; i < doorNumbersHolder.Length; i++)
                {
                    doorNumbersHolder[i] /= prevInput;
                }
                return;
            }

            userInputHolder.Push(userInputNum);
            for (int i = 1; i < doorNumbersHolder.Length; i++)
            {
                doorNumbersHolder[i] *= userInputNum;
            }
        }
    }
}
