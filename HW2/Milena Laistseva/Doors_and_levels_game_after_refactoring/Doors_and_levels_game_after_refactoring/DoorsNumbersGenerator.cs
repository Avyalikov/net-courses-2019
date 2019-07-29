using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doors_and_levels_game_after_refactoring
{
    public class DoorsNumbersGenerator: IDoorsNumbersGenerator
    {
        public int[] GenerateDoorsNumbers (int doorsAmount)
        {
            int[] doors = new int[doorsAmount];
            Random rand = new Random();
            for (int i = 0; i < (doorsAmount-1); i++)
            {
                doors[i] = rand.Next(2, 9);
            }
            doors[doorsAmount-1] = 0;

            return doors;
        }
    }
}
