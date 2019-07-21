using System;

namespace HW2_doors_and_levels_refactoring
{
    public class SettingsProvider : ISettingsProvider
    {
        private IInputOutputDevice inputOutputDevice;
        private IPhraseProvider phraseProvider;
        public SettingsProvider (IInputOutputDevice inputOutputDevice, IPhraseProvider phraseProvider)
        {
            this.inputOutputDevice = inputOutputDevice;
            this.phraseProvider = phraseProvider;
        }
        public GameSettings gameSettings()
        {
            int DoorsAmount;
            int PreviousLevelNumber;
            string ExitCode;
            inputOutputDevice.Print(phraseProvider.GetPhrase("DoorsAmount"));
            if (!Int32.TryParse(inputOutputDevice.InputValue(), out DoorsAmount)) DoorsAmount = 5;

            inputOutputDevice.Print(phraseProvider.GetPhrase("PreviousLevelNumber"));
            if (!Int32.TryParse(inputOutputDevice.InputValue(), out PreviousLevelNumber)) PreviousLevelNumber = 0;
            
            inputOutputDevice.Print(phraseProvider.GetPhrase("ExitCommand"));
            ExitCode = inputOutputDevice.InputValue();
            return new GameSettings(DoorsAmount, PreviousLevelNumber, ExitCode);
        }
    }
}