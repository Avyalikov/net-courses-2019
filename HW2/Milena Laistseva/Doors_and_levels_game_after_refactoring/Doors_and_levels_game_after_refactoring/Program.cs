using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doors_and_levels_game_after_refactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            ISettingsProvider settingsProvider = new SettingsProvider();
            GameSettings gameSettings = settingsProvider.GetGameSettings();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(gameSettings.Language);
            IInputOutputDevice inputOutputDevice = new ConsoleInputOutputDevice();
            IDoorsNumbersGenerator doorsNumbersGenerator = new DoorsNumbersGenerator();
            
            Game DoorsAndLevels = new Game(phraseProvider, inputOutputDevice, doorsNumbersGenerator, settingsProvider);
            DoorsAndLevels.Run();

            Console.ReadKey();
        }
    }
}
