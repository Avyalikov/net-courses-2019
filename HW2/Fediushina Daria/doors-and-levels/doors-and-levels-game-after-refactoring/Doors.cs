using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_and_levels_game_after_refactoring
{
    class Doors : INumbersArrayGenerator
    {
        private readonly GameSettings gameSettings;

        public Doors(ISettingsProvider settingsProvider)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
        }
        public int [] GenerateIntArr(int doorsAmount, int numRange)
        {
            Random r = new Random();
            int[] numbersArr = new int[doorsAmount];
            Boolean duplicateExists = true;
            while (duplicateExists)
            {                
                for (int i = 0; i < doorsAmount; i++)
                {
                    numbersArr[i] = r.Next(minValue: 1, maxValue: numRange);
                }
                duplicateExists = numbersArr.GroupBy(n => n).Any(g => g.Count() > 1);
            }
            return numbersArr;
        }

    }
}
