using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    class DoorsNumbersGenerator : IDoorsNumbersGenerator
    {
        public int[] GenerateDoorsNumbers(int doorsAmount, int prevLevelCode)
        {
            int[] doorsNumbers = new int[doorsAmount];
            var random = new Random();


            //if doors amount > 9 then the number of doors will contain random from 1 to amount
            //if doors amount <= 9 then the number of doors will contain random from 1 to 9
            int maxValue = doorsAmount <= 9 ? 9 : doorsAmount;  
            var listWithValues = new List<int>(Enumerable.Range(1, maxValue));


            for (int i = 0; i < doorsAmount-1; i++)
            {
                //Get random value from List and Remove that value from List
                int index = random.Next(1, maxValue);
                doorsNumbers[i] = listWithValues[index - 1];
                listWithValues.RemoveAt(index - 1);
                maxValue--;
            }

            //add the code of the previous level to the last position in the array
            doorsNumbers[doorsAmount - 1] = prevLevelCode;

            return doorsNumbers;
        }
    }
}
