namespace DoorsAndLevelsRef
{
    internal class DoorsNumbersGenerator : IArrayGenerator
    {
        private ISettingsProvider settingsProvider;

        public DoorsNumbersGenerator(ISettingsProvider settingsProvider)
        {
            this.settingsProvider = settingsProvider;
        }
    }
}