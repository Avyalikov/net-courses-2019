using System;
using System.Collections.Generic;

namespace DoorsAndLevelsRef
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputOutput inputOutput = new ConsoleInputOutput();
            IOperationWithData operationWithData = new OperationWithArrays();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(settingsProvider);
            IArrayGenerator arrayGenerator = new DoorsNumbersGenerator(settingsProvider, operationWithData);

            Game game = new Game(phraseProvider, inputOutput, settingsProvider, arrayGenerator);

            game.Run();

            Console.ReadKey();
        }
    }
}
