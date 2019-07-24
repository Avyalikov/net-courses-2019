namespace DoorsAndLevelsRefactoring
{
    using DoorsAndLevelsRefactoring.Provider;
    class Program
    {
        static void Main(string[] args)
        {
            SettingProvider settingProvider = new SettingProvider();

            GameLogic game = new GameLogic(
                phraseProvider:     new JsonPhraseProvider("Resource/LangRu.json"),
                inputAndOutput:     new ConsoleProvider(),
                getDoors:           new DoorsNumberRandom(settingProvider),
                doorsStorage:       new StackStorageProvider(),
                settingProvider:    settingProvider);

            game.StartGame();
        }
    }
}
