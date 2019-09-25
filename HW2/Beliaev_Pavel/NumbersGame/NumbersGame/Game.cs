using System;
using System.Collections.Generic;
using System.Linq;
using NumbersGame.Interfaces;

namespace NumbersGame
{   
    public enum InputCheckResult {Valid, Invalid, Exit, Info}
    public enum KeysForPhrases
    {
        Intro, Info , WinValueIs, ExitKey, InfoKey, EnterNumber,
        InvalidInput, SelectLang, FirstLvl, Goodbye, Win, CloseProg
    }

    public class Game
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutput ioModule;               

        private readonly GameSettings gameSettings;
                
        private int[] doorNumbersHolder;
        private Stack<int> userInputHolder;

        public Game
            (IPhraseProvider phraseProvider, IInputOutput ioModule, 
            ISettingsProvider settingsProvider, IDoorsNumbersGenerator doorsNumbersGenerator)
        {
            this.phraseProvider = phraseProvider;
            this.ioModule = ioModule;
            try
            {
                this.gameSettings = settingsProvider.GetGameSettings();
            }
            catch (ArgumentException ex)
            {
                ioModule.WriteOutput(ex.Message);
                this.gameSettings = null;
                return;
            }
            this.doorNumbersHolder = doorsNumbersGenerator.GenerateDoorsNumbers(this.gameSettings.DoorsAmount);
            this.userInputHolder = new Stack<int>();
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

        private bool TestLangPack()
        {
            try
            {
                foreach (KeysForPhrases phraseKey in Enum.GetValues(typeof(KeysForPhrases)))
                {
                    phraseProvider.GetPhrase(phraseKey, gameSettings.LangPackName);
                }
            }
            catch(System.FormatException ex)
            {
                ioModule.WriteOutput(ex.Message);
                return false;
            }
            return true;
            
        }

        private void ShowInfo()
        {
            ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.Info, gameSettings.LangPackName));
            ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.WinValueIs, gameSettings.LangPackName));
            ioModule.WriteOutput(gameSettings.WinNumber.ToString());
            ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.ExitKey, gameSettings.LangPackName));
            ioModule.WriteOutput(gameSettings.ExitButton);
            ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.InfoKey, gameSettings.LangPackName));
            ioModule.WriteOutput(gameSettings.InfoButton);
        }


        public void Run()
        {
            bool exit = false;
            string userInput;
            InputCheckResult checkResult;

            if (gameSettings == null) return;
            if (TestLangPack() == false) return;

            try
            {
            ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.Intro, gameSettings.LangPackName));
            }
            catch(ArgumentException ex)
            {
                ioModule.WriteOutput(ex.Message);
                return;
            }
            ShowInfo();

            while (!CheckWinCondition() && !exit)
            {
                string enterThisNum = phraseProvider.GetPhrase(KeysForPhrases.EnterNumber, gameSettings.LangPackName);
                foreach (int number in doorNumbersHolder)
                {
                    enterThisNum = enterThisNum + number + " ";
                }
                enterThisNum = enterThisNum + Environment.NewLine;
                ioModule.WriteOutput(enterThisNum);

                userInput = ioModule.ReadInput();
                if (string.IsNullOrEmpty(userInput))
                {
                    ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.InvalidInput, gameSettings.LangPackName));
                    continue;
                }
                checkResult = CheckIfInputIsValid(userInput);

                switch (checkResult)
                {
                    case InputCheckResult.Invalid :
                        {
                            ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.InvalidInput, gameSettings.LangPackName));
                            continue;
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
                ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.Win, gameSettings.LangPackName));                
            }

            ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.Goodbye, gameSettings.LangPackName));
            ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.CloseProg, gameSettings.LangPackName));
            
            ioModule.ReadKey();
        }


        private void NumbersChanger(string userInput)
        {
            int userInputNum = Convert.ToInt32(userInput);

            if (userInputNum == 0)
            {
                if (!userInputHolder.Any())
                {
                    ioModule.WriteOutput(phraseProvider.GetPhrase(KeysForPhrases.FirstLvl, gameSettings.LangPackName));
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
