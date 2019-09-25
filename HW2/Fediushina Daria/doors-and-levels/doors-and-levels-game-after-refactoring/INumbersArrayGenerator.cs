using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_and_levels_game_after_refactoring
{
    interface INumbersArrayGenerator
    {
        int[] GenerateIntArr(int numAmmount, int numRange); 
    }
}
