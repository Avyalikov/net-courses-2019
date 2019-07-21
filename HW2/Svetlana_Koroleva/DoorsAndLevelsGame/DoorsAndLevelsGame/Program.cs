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

            Game game = new Game(phraseProvider, ioComponent, doorsNumbersGenerator);

            while (game.Exit==false)
            {
                game.PlayGame();               
               
            }
            
            ioComponent.ReadInput();
        }
    }
}
