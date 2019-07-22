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
            IPhraseProvider phraseProvider = new JsonPhraseProvider();

            Game DoorsAndLevels = new Game(phraseProvider);
            DoorsAndLevels.Run();

            Console.ReadKey();
        }
    }
}
