using System;
using System.Collections.Generic;

namespace HW2_doors_and_levels_refactoring
{
    class Program
    {
        static void Main()
        {
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(settingsProvider);
            IInputOutputDevice inputOutputDevice = new ConsoleIODevice();
            IStartNumbersGenerator startNumbersGenerator = new StartNumbersGenerator(settingsProvider);
            INumbersChanger numbersChanger = new NumbersChanger(inputOutputDevice,phraseProvider, settingsProvider);

            var game = new Game(phraseProvider, inputOutputDevice, startNumbersGenerator,numbersChanger, settingsProvider);
            game.Run();
        }
    }
}
