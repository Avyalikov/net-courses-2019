namespace ConsoleCanvas
{
    using ConsoleCanvas.Drawers;
    using ConsoleCanvas.Interfaces;

    public class Program
    {
        private const string SettingsFilePath = "settings.json";

        public static void Main(string[] args)
        {
            IDictionaryParser jsonParser = new JsonFileParser();
            ISettingsProvider settingsProvider = new FileSettingsProvider(jsonParser, SettingsFilePath);
            ISettings settings = settingsProvider.GetSettings();
            IDrawManager drawManager = new ConsoleDrawManager();
            IKeyboardManager keyboardManager = new KeyboardManager();
            IPhraseProvider phraseProvider = new PhraseProvider(jsonParser, settings.Language);

            IObjectDrawer canvasDrawer = new CanvasDrawer(drawManager);
            IObjectDrawer gooseDrawer = new GooseDrawer(drawManager);
            IObjectDrawer dotDrawer = new DotDrawer(drawManager, settings.DotXOffset, settings.DotYOffset);
            IObjectDrawer verticalLineDrawer = new VerticalLineDrawer(drawManager, settings.VerticalLineXOffsetPercent);
            IObjectDrawer horizontalLineDrawer = new HorizontalLineDrawer(drawManager, settings.HorizontalLineYOffsetPercent);
            IBoard board = settings.Board;

            drawManager.Initialize();
            phraseProvider.Initialize();

            IConsoleDrawer consoleDrawer = new ConsoleDrawer(
                drawManager,
                keyboardManager,
                phraseProvider,
                canvasDrawer,
                dotDrawer,
                verticalLineDrawer,
                horizontalLineDrawer,
                gooseDrawer,
                board);

            consoleDrawer.Run();
        }
    }
}