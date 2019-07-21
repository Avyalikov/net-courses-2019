using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame
{
    class SimpleRandomIntArrayGenerator : IArrayGenerator<int>
    {
        private Random rand = new Random();
        int[] IArrayGenerator<int>.GetArray(int size)
        {
            int[] numberArray = new int[size];

            for (int i = 0; i < numberArray.Length-1; i++)
            {
                numberArray[i] = rand.Next(1, 10);
            }
            numberArray[numberArray.Length-1] = 0;

            return numberArray;
        }
    }
}
