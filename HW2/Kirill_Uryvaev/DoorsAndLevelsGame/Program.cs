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
            IInputOutputProvider inputOutputProvider = new ConsoleIOProvider();
            INumberGenerator numberGenerator = new UniformNumberGenerator();
            GameManager game = new GameManager(numbers, phraseProvider, inputOutputProvider, numberGenerator);
            game.Run();
        }
    }
}
