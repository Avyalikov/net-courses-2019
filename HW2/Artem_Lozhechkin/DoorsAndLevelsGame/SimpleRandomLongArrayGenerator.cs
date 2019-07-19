using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame
{
    class SimpleRandomLongArrayGenerator : IArrayGenerator<long>
    {
        private Random rand = new Random();
        long[] IArrayGenerator<long>.GetArray(int size)
        {
            long[] numberArray = new long[size];

            for (int i = 0; i < numberArray.Length-1; i++)
            {
                numberArray[i] = rand.Next(1, 10);
            }
            numberArray[numberArray.Length-1] = 0;

            return numberArray;
        }
    }
}
