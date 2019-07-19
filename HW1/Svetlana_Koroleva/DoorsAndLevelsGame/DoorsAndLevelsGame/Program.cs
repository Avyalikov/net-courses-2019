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
            Game game = new Game();

            while (game.Exit==false)
            {
                game.PlayGame();               
               
            }

            Console.ReadLine();
        }
    }
}
