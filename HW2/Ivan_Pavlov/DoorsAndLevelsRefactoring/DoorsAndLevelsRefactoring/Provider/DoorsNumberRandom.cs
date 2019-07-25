namespace DoorsAndLevelsRefactoring.Provider
{
    using DoorsAndLevelsRefactoring.Interface;
    using System;
    using System.Collections.Generic;

    class DoorsNumberRandom : IGetDoorsNumber
    {

        private readonly GameSettings gameSettings;

        public DoorsNumberRandom(ISettingProvider settingProvider)
        {
            this.gameSettings = settingProvider.GetGameSettings();
        }

        public int[] GetDoorsNumber(int doorsAmount)
        {
            List<int> nums = new List<int>();
            Random rnd = new Random();

            for(int i = 0; i < doorsAmount - 1; )
            {
                int n = rnd.Next(1, doorsAmount * 2);
                if (nums.Contains(n))
                    continue;
                nums.Add(n);
                i++;
            }

           nums.Add(gameSettings.ExitDoorNumber);

            return nums.ToArray();
        }
    }
}
