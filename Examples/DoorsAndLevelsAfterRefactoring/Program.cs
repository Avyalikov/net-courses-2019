namespace DoorsAndLevelsAfterRefactoring
{
    class Program
    {
        
        static void Main()
        {

            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            IInputOutputDevice inputOutputDevice = new ConsoleInputOutputDevice();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IDoorsNumbersGenerator doorsNumbersGenerator = new DoorsNumbersGenerator(settingsProvider);

            var game = new Game(phraseProvider, inputOutputDevice, settingsProvider, doorsNumbersGenerator);
            game.Run();
        }
    }
}
