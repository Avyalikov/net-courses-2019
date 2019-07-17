using System;

namespace DoorsAndLevelsAfterRefactoring
{
    public class DoorsNumbersGenerator : IDoorsNumbersGenerator
    {
        public int[] GenerateDoorsNumbers(int doorsAmount)
        {
            int[] nums = new int[doorsAmount];
            Random random = new Random();
            //fills array with random numbers from 1 to 9
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = random.Next(1, 9);
            }
            //adds 0 in random place of array
            nums[random.Next(0, nums.Length - 1)] = 0;
            return nums;
        }
    }
}
