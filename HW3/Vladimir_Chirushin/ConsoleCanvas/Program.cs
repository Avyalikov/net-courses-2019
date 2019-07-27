using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCanvas
{
    public struct Canvas
    {
        public int x1;  //upper left corner
        public int y1;

        public int x2;  //bottom right corner
        public int y2;

        public Canvas(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;

            this.x2 = x2;
            this.y2 = y2;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string settingsFilePath = "settings.json";

            IDrawManager drawManager = new DrawManager();
            IKeyboardManager keyboardManager = new KeyboardManager();
            drawManager.DrawInitiate();
            IDrawCanvasClass drawCanvas = new DrawCanvasClass(drawManager);
            IFileParser jsonParser = new JsonFileParser();
            ISettingsProvider settingsProvider = new SettingsProvider(jsonParser, settingsFilePath);
            ISettings settings = settingsProvider.GetSettings();
            IDrawGooseClass drawGooseClass = new DrawGooseClass(drawManager);
            drawGooseClass.InitiateGoose();
            IPhraseProvider phraseProvider = new PhraseProvider(jsonParser, settings.GetLanguage());
            phraseProvider.InitiatePhrases();
            IDrawDotClass dotDrawClass = new DrawDotClass(drawManager, settings.GetDotXOffset(), settings.GetDotYOffset());
            IDrawVerticalLineClass drawVerticalLineClass = new DrawVerticalLineClass(drawManager, settings.GetVerticalLineXOffset());
            IDrawHorizontalLineClass drawHorizontalLineClass = new DrawHorizontalLineClass(drawManager, settings.GetHorizontalLineYOffset());

            Canvas canvas = settings.GetCanvas();
            IConsoleDrawer consoleDrawer = new ConsoleDrawer(
                drawManager,
                keyboardManager,
                phraseProvider,
                drawCanvas,
                dotDrawClass,
                drawVerticalLineClass,
                drawHorizontalLineClass,
                drawGooseClass,
                canvas
                );
            consoleDrawer.Run();
        }
    }
}

