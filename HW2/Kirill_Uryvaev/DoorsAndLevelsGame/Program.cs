using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            IInputOutputProvider inputOutputProvider = new ConsoleIOProvider();
            INumberGenerator numberGenerator = new UniformNumberGenerator();
            ISettingsProvider settingsProvider = new FileSettingsProvider();

            GameManager game = new GameManager(phraseProvider, inputOutputProvider, numberGenerator, settingsProvider);
            game.Run();
        }
    }
}
