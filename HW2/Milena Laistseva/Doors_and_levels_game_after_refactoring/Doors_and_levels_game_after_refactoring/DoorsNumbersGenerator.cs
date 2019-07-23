using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doors_and_levels_game_after_refactoring
{
    public class DoorsNumbersGenerator: IDoorsNumbersGenerator
    {
        public int[] GenerateDoorsNumbers ()
        {
            int[] doors = new int[5];
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                doors[i] = rand.Next(2, 9);
            }
            doors[4] = 0;

            return doors;
        }
    }
}
