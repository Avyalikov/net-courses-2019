namespace DoorsAndLevelsRefactoring
{
    using DoorsAndLevelsRefactoring.Provider;
    class Program
    {
        static void Main(string[] args)
        {
            SettingProvider settingProvider = new SettingProvider();

            GameLogic game = new GameLogic(
                phraseProvider:     new JsonPhraseProvider(string.Format("Resource/Lang{0}.json", settingProvider.GetGameSettings().Lang)),
                inputAndOutput:     new ConsoleProvider(),
                getDoors:           new DoorsNumberRandom(settingProvider),
                doorsStorage:       new StackStorageProvider(),
                settingProvider:    settingProvider);

            game.StartGame();
        }
    }
}
