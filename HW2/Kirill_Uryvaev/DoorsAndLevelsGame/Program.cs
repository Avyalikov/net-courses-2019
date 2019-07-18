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
            Console.WriteLine(phraseProvider.GetPhrase("Rules"));
            Console.WriteLine(game.ShowCurrentLevel());
            string key = "";
            int pickedDoor = 0;
            while (!key.Equals("e"))
            {
                key = Console.ReadLine();
                bool isNumeric = int.TryParse(key, out pickedDoor);
                if (isNumeric)
                {
                    Console.WriteLine(game.PickDoor(pickedDoor));
                }
                else if (!key.ToLower().Equals("e"))
                {
                    Console.WriteLine(phraseProvider.GetPhrase("IncorrectInput"));
                }
            }
        }
    }
}
