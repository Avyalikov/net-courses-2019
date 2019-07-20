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
            Game game = new Game(phraseProvider);

            while (game.Exit==false)
            {
                game.PlayGame();               
               
            }

            Console.ReadLine();
        }
    }
}
