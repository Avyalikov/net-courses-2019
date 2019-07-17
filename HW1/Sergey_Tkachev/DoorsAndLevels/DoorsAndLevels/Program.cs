using System;
using System.Collections.Generic;

namespace DoorsAndLevels
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            game.Run();

            Console.ReadKey();
        }
    }
}
