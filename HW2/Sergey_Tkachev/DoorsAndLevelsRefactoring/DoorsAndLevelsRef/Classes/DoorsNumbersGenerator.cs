namespace DoorsAndLevelsRef
{
    internal class DoorsNumbersGenerator : IArrayGenerator
    {
        private readonly GameSettings gameSettings;

        public DoorsNumbersGenerator(ISettingsProvider settingsProvider)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
        }

        public int[] GenerateArray(int elementsAmount)
        {
            throw new System.NotImplementedException();
        }
    }
}