using System;
using System.Collections.Generic;

namespace HW2_doors_and_levels_refactoring
{
    class Program
    {
        static void Main()
        {
            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            IInputOutputDevice inputOutputDevice = new ConsoleIODevice();
            ISettingsProvider settingsProvider = new SettingsProvider(inputOutputDevice, phraseProvider);
            inputOutputDevice.Print(phraseProvider.GetPhrase("Welcome"));
            GameSettings gameSettings = settingsProvider.gameSettings(); //settings setup in a runtime
            IStartNumbersGenerator startNumbersGenerator = new StartNumbersGenerator(gameSettings);
            INumbersChanger numbersChanger = new NumbersChanger(inputOutputDevice,phraseProvider, gameSettings);

            var game = new Game(phraseProvider, inputOutputDevice, startNumbersGenerator,numbersChanger, gameSettings);
            game.Run();
        }
    }
}
