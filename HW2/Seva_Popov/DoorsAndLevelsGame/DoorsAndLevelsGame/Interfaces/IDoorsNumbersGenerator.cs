using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame.Interfaces
{
    interface IDoorsNumbersGenerator
    {
        int[] GenerateDoorsNumbers(int doorsAmount);
    }
}
