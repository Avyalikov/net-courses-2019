using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    class UniformNumberGenerator : INumberGenerator
    {
        public int[] GetNumbers(int count, int maxValue, int exitDoorNumber)
        {
            int[] numberArray = new int[count];
            Random rnd = new Random();
            numberArray = Enumerable.Range(1, maxValue).OrderBy(x => rnd.Next()).Take(count).ToArray();
            if (numberArray.Contains(exitDoorNumber))
            {
                int tempIndex = Array.IndexOf(numberArray, exitDoorNumber);
                numberArray[tempIndex] = numberArray[count - 1];
            }
            numberArray[count - 1] = exitDoorNumber;
            return numberArray;
        }
    }
}
