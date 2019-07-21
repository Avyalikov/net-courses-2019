using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    class DoorsNumbersGenerator : IDoorsNumbersGenerator
    {
        public int[] GenerateDoorsNumbers(int doorsAmount)
        {
            int[] doorsNumbers = new int[doorsAmount];
            var random = new Random();
            int maxValue = 9;  //
            var listWithValues = new List<int>(Enumerable.Range(1, 9));


            for (int i = 0; i < doorsAmount-1; i++)
            {
                //Get random value from List and Remove that value from List
                int index = random.Next(1, maxValue);
                doorsNumbers[i] = listWithValues[index - 1];
                listWithValues.RemoveAt(index - 1);
                if (doorsAmount <= 9) 
                    maxValue--; //max index value decrease
            }

            doorsNumbers[doorsAmount - 1] = 0;

            return doorsNumbers;
        }
    }
}
