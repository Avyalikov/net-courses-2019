namespace DoorsAndLevelsRefactoring.Provider
{
    using DoorsAndLevelsRefactoring.Interface;
    using System;

    class DoorsNumberRandom : IGetDoorsNumber
    {

        private readonly GameSettings gameSettings;

        public DoorsNumberRandom(ISettingProvider settingProvider)
        {
            this.gameSettings = settingProvider.GetGameSettings();
        }

        public int[] GetDoorsNumber(int doorsAmount)
        {
            int[] nums = new int[doorsAmount];
            Random rnd = new Random();

            for(int i = 0; i < nums.Length; i++)
            {
                nums[i] = rnd.Next(1, 9);
            }

            nums[rnd.Next(0, nums.Length - 1)] = gameSettings.ExitDoorNumber;

            return nums;
        }
    }
}
