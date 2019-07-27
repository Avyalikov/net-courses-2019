using System;

namespace ConsoleCanvas
{

    public delegate void DrawDelegate(Canvas canvas);

    public class ConsoleDrawer : IConsoleDrawer
    {
        private readonly IDrawManager drawManager;
        private readonly IPhraseProvider phraseProvider;
        private readonly IDrawCanvasClass drawCanvas;
        private readonly IDrawDotClass dotDrawClass;
        private readonly IDrawVerticalLineClass drawVerticalLineClass;
        private readonly IDrawHorizontalLineClass drawHorizontalLineClass;
        private readonly IDrawGooseClass drawGooseClass;
        private readonly IKeyboardManager keyboardManager;
        private readonly Canvas canvas;

        private DrawDelegate drawingDelegates = null;

        private ConsoleKeyInfo consoleKeyPressed;
        public ConsoleDrawer(
            IDrawManager drawManager,
            IKeyboardManager keyboardManager,
            IPhraseProvider phraseProvider,
            IDrawCanvasClass drawCanvas,
            IDrawDotClass dotDrawClass,
            IDrawVerticalLineClass drawVerticalLineClass,
            IDrawHorizontalLineClass drawHorizontalLineClass,
            IDrawGooseClass drawGooseClass,
            Canvas canvas)
        {
            this.drawManager = drawManager;
            this.keyboardManager = keyboardManager;
            this.phraseProvider = phraseProvider;
            this.drawCanvas = drawCanvas;
            this.dotDrawClass = dotDrawClass;
            this.drawVerticalLineClass = drawVerticalLineClass;
            this.drawHorizontalLineClass = drawHorizontalLineClass;
            this.drawGooseClass = drawGooseClass;
            this.canvas = canvas;

        }

        public void Run()
        {
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.welcome));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.canvasDrawMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.dotDrawMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.verticalDrawMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.horizontalDrawMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.gooseDrawMessage));
            drawManager.WriteLine(String.Empty);
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.canvasErraseMesage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.dotErraseMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.verticalErraseMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.horizontalErraseMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.gooseErraseMessage));



            do
            {
                consoleKeyPressed = keyboardManager.ReadKey();

                switch (consoleKeyPressed.Key) //Switch on Key enum
                {
                    case ConsoleKey.D1: //Key "1"
                        drawingDelegates += new DrawDelegate(drawCanvas.DrawCanvas);
                        break;

                    case ConsoleKey.D2: //Key "2"
                        drawingDelegates += new DrawDelegate(dotDrawClass.DrawDot);
                        break;

                    case ConsoleKey.D3: //Key "3"
                        drawingDelegates += new DrawDelegate(drawVerticalLineClass.DrawVerticalLine);
                        break;

                    case ConsoleKey.D4: //Key "4"
                        drawingDelegates += new DrawDelegate(drawHorizontalLineClass.DrawHorizontallLine);
                        break;
                    case ConsoleKey.D5: //Key "5"
                        drawingDelegates += new DrawDelegate(drawGooseClass.DrawGoose);
                        break;



                    case ConsoleKey.Q: //Key "Q"
                        drawingDelegates -= new DrawDelegate(drawCanvas.DrawCanvas);
                        break;

                    case ConsoleKey.W: //Key "W"
                        drawingDelegates -= new DrawDelegate(dotDrawClass.DrawDot);
                        break;

                    case ConsoleKey.E: //Key "E"
                        drawingDelegates -= new DrawDelegate(drawVerticalLineClass.DrawVerticalLine);
                        break;

                    case ConsoleKey.R: //Key "R"
                        drawingDelegates -= new DrawDelegate(drawHorizontalLineClass.DrawHorizontallLine);
                        break;
                    case ConsoleKey.T: //Key "R"
                        drawingDelegates -= new DrawDelegate(drawGooseClass.DrawGoose);
                        break;
                    default:
                        break;
                }
                drawManager.ProceedDrawing(drawingDelegates, canvas);
            } while (consoleKeyPressed.Key != ConsoleKey.Escape);

        }
    }
}