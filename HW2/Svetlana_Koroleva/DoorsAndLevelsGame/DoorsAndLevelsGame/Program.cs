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
           
            IInputOutputComponent ioComponent = new ConsoleIOComponent();
            IDoorsNumbersGenerator doorsNumbersGenerator = new DoorsGenerator();
            ISettingsProvider settings = new JSONSettingsProvider();
            IPhraseProvider phraseProvider = new JSONPhraseProvider(settings.GetGameSettings().Language);
            Game game = new Game(phraseProvider, ioComponent, doorsNumbersGenerator, settings);
            game.PlayGame();  
                      
            ioComponent.ReadInput();
        }
    }
}
