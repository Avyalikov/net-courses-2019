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
            Game game = new Game
            {
                Level = 0
            };

            game.levelsResults = new Dictionary<int, int[]>();

            //TO DO Create condition for exit
            while (true)
            {
                game.PlayGame();
                game.ShowCurrentResult();
            }

            
                       
           
        }
    }
}
