using System;
using System.Collections.Generic;

namespace DoorsAndLevels
{
    class DoorsNumGenerator : Interfaces.IDoorsGenerator
    {
        public List<int> GetDoorsNumbers(int doorsCount, int minRandom, int maxRandom)
        {
            List<int> doorsNumbers = new List<int>();
            Random genNum = new Random();
            doorsNumbers.Add(0);

            for (int i = 0; i < doorsCount - 1; i++)
            {
                while (true)
                {
                    int number = genNum.Next(minRandom, maxRandom);
                    if (!doorsNumbers.Contains(number))
                    {
                        doorsNumbers.Add(number);
                        break;
                    }
                }
            }

            return doorsNumbers;
        }
    }
}
