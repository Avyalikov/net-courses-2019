namespace CreateDoorsAndLevels
{
    class Program
    {
        static void Main()
        {
            Interfaces.IPhraseProvider phraseProvider = new Modules.JsonPhraseProvider();
            Interfaces.IInputOutputDevice inputOutputDevice = new Modules.ConsoleInputOutputDevice();
            Interfaces.IDoorsNumbersGenerator doorsNumbersGenerator = new Modules.DoorsNumbersGenerator();
            Interfaces.ISettingsProvider settingsProvider = new Modules.JsonSettingsProvider();

            new Game(
                phraseProvider: phraseProvider, 
                inputOutputDevice: inputOutputDevice,
                doorsNumbersGenerator: doorsNumbersGenerator, 
                settingsProvider: settingsProvider
                ) { }.Run();

            inputOutputDevice.ReadKey(); // pause
        }        
    }
}
