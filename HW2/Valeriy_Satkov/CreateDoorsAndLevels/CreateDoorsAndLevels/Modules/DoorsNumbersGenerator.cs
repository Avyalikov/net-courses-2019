using System;
using System.Collections.Generic;

namespace CreateDoorsAndLevels.Modules
{
    class DoorsNumbersGenerator : Interfaces.IDoorsNumbersGenerator
    {
        public List<int> generateDoorsNumbers(int doorsAmount)
        {
            List<int> result = new List<int>();
            int randomNumber;

            Random random = new Random();

            for (int i = 0; i < doorsAmount - 1; i++)
            {
                while (result.Contains(randomNumber = random.Next(1, doorsAmount * 2)));
                result.Add(randomNumber);
            }

            result.Add(0);

            return result;
        }
    }
}
