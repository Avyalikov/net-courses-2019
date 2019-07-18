using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    class UniformNumberGenerator : INumberGenerator
    {
        public int[] GetNumbers(int count, int maxValue)
        {
            int[] numberArray = new int[count];
            Random rnd = new Random();
            numberArray = Enumerable.Range(1, maxValue).OrderBy(x => rnd.Next()).Take(count).ToArray();
            numberArray[count - 1] = 0;
            return numberArray;
        }
    }
}
