using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyType = System.Int32;

namespace HW2
{
    public class Rand : IRandomProvider
    {

        public MyType[] rand(int NumberOfValues, int MinRand, int MaxRand)
        {
            Random rand = new Random();
            MyType[] numbers = new MyType[NumberOfValues];
            bool allValuesRandom = false;
            int randCycleCounter = 0;

            do
            {
                for (int i = 0; i < numbers.Length - 1; ++i)
                {
                    numbers[i] = (MyType)rand.Next(MinRand, MaxRand);
                }

                numbers[numbers.Length - 1] = 0;

                if (numbers.Length == numbers.Distinct().Count())
                {
                    allValuesRandom = true;
                }
            }
            while (!allValuesRandom && randCycleCounter < 1000);

            return numbers;
        }
    }
}