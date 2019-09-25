using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    public interface  IDoorsNumbersGenerator
    {
        int[] GenerateNumbers(int doorsQuantity,int maxNumber);
    }
}
