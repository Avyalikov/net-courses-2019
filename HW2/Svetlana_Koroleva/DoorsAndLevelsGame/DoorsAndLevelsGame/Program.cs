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
            IPhraseProvider phraseProvider = new JSONPhraseProvider();
            IInputOutputComponent ioComponent = new ConsoleIOComponent();
            IDoorsNumbersGenerator doorsNumbersGenerator = new DoorsGenerator();
            ISettingsProvider settings = new JSONSettingsProvider();


            Game game = new Game(phraseProvider, ioComponent, doorsNumbersGenerator, settings);

            while (game.Exit==false)
            {
                game.PlayGame();               
               
            }
            
            ioComponent.ReadInput();
        }
    }
}
