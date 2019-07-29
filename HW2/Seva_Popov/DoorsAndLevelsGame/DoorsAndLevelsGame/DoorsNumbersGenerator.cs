using DoorsAndLevelsGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame
{
    class DoorsNumbersGenerator : IDoorsNumbersGenerator
    {
       public int[] GenerateDoorsNumbers(int doorsAmount)
        {
            int[] doorNumbers = new int[doorsAmount];
            Random random = new Random();

            for (int i = 0; i < doorNumbers.Length; i++)
            {
                doorNumbers[i] = random.Next(1, 9);
            }

            doorNumbers[doorNumbers.Length - 1] = 0;

            return doorNumbers;
        }
    }
}
