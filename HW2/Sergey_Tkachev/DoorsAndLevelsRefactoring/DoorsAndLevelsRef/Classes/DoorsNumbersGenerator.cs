using System;

namespace DoorsAndLevelsRef
{
    internal class DoorsNumbersGenerator : IArrayGenerator
    {
        private readonly GameSettings gameSettings;
        private readonly OperationWithArrays operationWithArrays;

        public DoorsNumbersGenerator(ISettingsProvider settingsProvider, IOperationWithData operationWithData)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
            this.operationWithArrays = (OperationWithArrays) operationWithData;
        }

        /// <summary>Generates Array of integers. Last element always 0.</summary>
        /// <param name="elementsAmount">Amount of elements in Array</param>
        /// <returns></returns>
        public int[] GenerateArray(int elementsAmount)
        {
            int[] generatedArray = new int[elementsAmount];
            Random random = new Random();
            int num;
            for (int i = 0; i < generatedArray.Length - 1; i++)
            {
                do
                {
                    num = random.Next(1, gameSettings.maxInitialValue);
                } while (operationWithArrays.Contains(generatedArray, num));
                generatedArray[i] = num;
            }
            generatedArray[generatedArray.Length - 1] = 0;
            return generatedArray;
        }
    }
}