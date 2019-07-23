using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    public interface INumberGenerator
    {
        int[] GetNumbers(int count, int maxValue, int exitDoorNumber);
    }
}
