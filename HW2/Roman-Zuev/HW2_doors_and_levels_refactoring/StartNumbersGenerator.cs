using System;

namespace HW2_doors_and_levels_refactoring
{
    public class StartNumbersGenerator : IStartNumbersGenerator
    {
        private readonly GameSettings gameSettings;

        public StartNumbersGenerator(ISettingsProvider settingsProvider)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
        }

        public int[] GenerateStartNumbers(int doorsAmount)
        {
            int[] nums = new int[doorsAmount];
            Random random = new Random();
            //fills array with random numbers from 1 to 9
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = random.Next(1, 6);
            }
            //adds 0 in random place of arraysss
            nums[random.Next(0, nums.Length - 1)] = gameSettings.PreviousLevelNumber;
            return nums;
        }
    }
}