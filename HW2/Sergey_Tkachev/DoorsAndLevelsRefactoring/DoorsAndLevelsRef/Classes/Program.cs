using System;
using System.Collections.Generic;

namespace DoorsAndLevelsRef
{
    class Program
    {
        static void Main(string[] args)
        {
            IOperationWithData operationWithData = new OperationWithArrays();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(settingsProvider);
            IInputOutput inputOutput = new ConsoleInputOutput(phraseProvider);
            IArrayGenerator arrayGenerator = new DoorsNumbersGenerator(settingsProvider, operationWithData);

            Game game = new Game(phraseProvider, inputOutput, settingsProvider, arrayGenerator, operationWithData);

            game.Run();
        }
    }
}
