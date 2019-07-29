//using System;

//namespace HW2_doors_and_levels_refactoring
//{
//    public class SettingsProvider : ISettingsProvider
//    {
//        private IInputOutputDevice inputOutputDevice;
//        private IPhraseProvider phraseProvider;
//        public SettingsProvider (IInputOutputDevice inputOutputDevice, IPhraseProvider phraseProvider)
//        {
//            this.inputOutputDevice = inputOutputDevice;
//            this.phraseProvider = phraseProvider;
//        }
//        public GameSettings gameSettings()//provides User settings to a GameSettings object in a runtime
//        {
//            int DoorsAmount;
//            int PreviousLevelNumber;
//            int MaxLevel;
//            string ExitCode;
//            inputOutputDevice.Print(phraseProvider.GetPhrase("DoorsAmount"));
//            if (!Int32.TryParse(inputOutputDevice.InputValue(), out DoorsAmount)) DoorsAmount = 5;//default value in case of wrong input

//            inputOutputDevice.Print(phraseProvider.GetPhrase("PreviousLevelNumber"));
//            if (!Int32.TryParse(inputOutputDevice.InputValue(), out PreviousLevelNumber)) PreviousLevelNumber = 0;//default value in case of wrong input

//            inputOutputDevice.Print(phraseProvider.GetPhrase("MaxLevel"));
//            if (!Int32.TryParse(inputOutputDevice.InputValue(), out MaxLevel)) MaxLevel = 4;
//            if (MaxLevel < 1 || MaxLevel > 4) MaxLevel = 4; //default for MaxLevel

//            inputOutputDevice.Print(phraseProvider.GetPhrase("ExitCommand"));
//            ExitCode = inputOutputDevice.InputValue();
//            if (string.IsNullOrEmpty(ExitCode)) ExitCode = "x";
//            return new GameSettings(DoorsAmount, PreviousLevelNumber, ExitCode, MaxLevel);
//        }
//    }
//}