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
            GameManager game = new GameManager(numbers);
            Console.WriteLine(new string('#', 45));
            Console.WriteLine("The rules:");
            Console.WriteLine("To exit the game, enter 'e' character.");
            Console.WriteLine("To go on the next level enter door number.");
            Console.WriteLine("To go on the previous level enter zero.");
            Console.WriteLine("If you already at zero level,");
            Console.WriteLine("Going on previous level will regenerate doors");
            Console.WriteLine(new string('#', 45) + '\n');
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
                else if (!key.Equals("e"))
                {
                    Console.WriteLine("Incorrect input");
                }
            }
        }
    }
}
