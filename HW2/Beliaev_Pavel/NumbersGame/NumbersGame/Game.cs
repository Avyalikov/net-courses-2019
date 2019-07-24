using System;
using System.Collections.Generic;
using System.Linq;
using NumbersGame.Interfaces;

namespace NumbersGame
{   public enum Language {Eng, Rus}
    public class Game
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutput ioModule;        
        private readonly IDoorsNumbersGenerator doorsNumbersGenerator;

        private readonly GameSettings gameSettings;
        private readonly Language ChosenLang;
        

        int[] doorNumbersHolder;
        Stack<int> userInputHolder = new Stack<int>();

        public void LevelUp(int a)
        {
            
        }

        public bool CheckInputNumber(int i)
        {
            
        }

        public bool CheckWinCondition()
        {
         //You win when one of the doors >= max number   
        }

        public Game
            (IPhraseProvider phraseProvider, IInputOutput ioModule, 
            ISettingsProvider settingsProvider, IDoorsNumbersGenerator doorsNumbersGenerator, Language ChosenLang)
        {
            this.phraseProvider = phraseProvider;
            this.ioModule = ioModule;

            this.gameSettings = settingsProvider.GetGameSettings();
            this.doorNumbersHolder = doorsNumbersGenerator.GenerateDoorsNumbers(this.gameSettings.DoorsAmount);
            this.ChosenLang = ChosenLang;
        }

        public void Run()
        {


        }

        private void NumbersChanger(string UserInput)
        {

        }
    }
}
