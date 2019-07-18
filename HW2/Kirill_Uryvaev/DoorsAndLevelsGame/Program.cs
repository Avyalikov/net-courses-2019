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
            int numbers = 5;
            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            GameManager game = new GameManager(numbers, phraseProvider);
            game.Run();
        }
    }
}
