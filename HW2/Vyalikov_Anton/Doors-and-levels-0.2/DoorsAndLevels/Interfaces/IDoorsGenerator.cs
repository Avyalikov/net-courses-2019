using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevels.Interfaces
{
    interface IDoorsGenerator
    {
        List<int> GetDoorsNumbers(int doorsCount, int minRandom, int maxRandom);
    }
}
