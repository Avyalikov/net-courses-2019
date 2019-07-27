using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_and_levels_game_after_refactoring
{
    class doors_and_levels_game_after_refactoring7
    {
        static void Main(string[] args)
        {
            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            IInputOutputDevice inputOutputDevice = new ConsoleInputOutputDevice();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IDoorsNumbersGenerator doorsNumbersGenerator = new DoorsNumbersGenerator(settingsProvider);

            var game = new Game(phraseProvider, inputOutputDevice, settingsProvider, doorsNumbersGenerator);
            game.Run();
        }
    }
}
