using System;
using System.Collections.Generic;

namespace DoorsAndLevelsRef
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputOutput inputOutput = new ConsoleInputOutput();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(settingsProvider);
            IArrayGenerator arrayGenerator = new DoorsNumbersGenerator(settingsProvider);

            Game game = new Game(phraseProvider, inputOutput, settingsProvider, arrayGenerator);

            game.Run();

            Console.ReadKey();
        }
    }
}
