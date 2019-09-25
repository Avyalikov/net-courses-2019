using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This class implements IArrayGenerator<int> and is used for generating an array of integers.
    /// </summary>
    class SimpleRandomIntArrayGenerator : IArrayGenerator<int>
    {
        /// <summary>
        /// Random is used for filling an array.
        /// </summary>
        private Random rand = new Random();
        /// <summary>
        /// This method returns an array of integers in the range of 1-10. Last element is always 0.
        /// </summary>
        /// <param name="size">Size of array.</param>
        /// <returns>Returns an array of random integers.</returns>
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
