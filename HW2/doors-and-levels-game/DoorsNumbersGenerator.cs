using System;

namespace doors_and_levels_game
{
    public class DoorsNumbersGenerator : IDoorsNumbersGenerator
    {
        public readonly GameSettings gameSettings;

        public getSettingsFromFile (ISettingsProvider settingsProvider)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
        }

        public int[] NumbersGenerate(int DoorsAmount)
        {
            int[] numbersArr = new int[DoorsAmount];
            Random r = new Random();
            for (int i = 0; i < DoorsAmount; i++)
            {
                numbersArr.Add(r.Next(gameSettings.UpperThresholdValue));
            }
            numbersArr[r.Next(DoorsAmount - 1)] = gameSettings.BackToPreviousLevelDoorNumber;
            return numbersArr;
        }
    }
}

