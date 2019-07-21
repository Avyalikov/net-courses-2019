using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    public class DoorsGenerator:IDoorsNumbersGenerator
    {
        public int[] generatedNumbers(int quantity) {

            int[] generatedNumbers = new int[quantity];
            Random random = new Random();

            for (int i = 0; i < generatedNumbers.Length - 1; i++)
            {
                generatedNumbers[i] = random.Next(1, 9);
            }

            generatedNumbers[quantity-1] = 0;

            return generatedNumbers;
        }

    }
    }

