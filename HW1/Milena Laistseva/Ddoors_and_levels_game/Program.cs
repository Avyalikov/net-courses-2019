using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_and_levels_game
{
    class Program
    {
        static void Main(string[] args)
        {
            Game DoorsAndLevels = new Game();
            DoorsAndLevels.Run();

            Console.ReadKey();
        }
    }
}
