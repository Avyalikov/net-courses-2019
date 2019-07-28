using ConsoleCanvas.Interfaces;
using System;

namespace ConsoleCanvas
{
    public delegate void DrawDelegate(IBoard board);

    public class ConsoleDrawer : IConsoleDrawer
    {
        private readonly IDrawManager drawManager;
        private readonly IPhraseProvider phraseProvider;
        private readonly IObjectDrawer canvasDrawer;
        private readonly IObjectDrawer dotDrawer;
        private readonly IObjectDrawer verticalLineDrawer;
        private readonly IObjectDrawer horizontalLineDrawer;
        private readonly IObjectDrawer gooseDrawer;
        private readonly IKeyboardManager keyboardManager;
        private readonly IBoard board;

        private DrawDelegate drawingDelegates = null;
        private ConsoleKeyInfo consoleKeyPressed;

        public ConsoleDrawer(
            IDrawManager drawManager,
            IKeyboardManager keyboardManager,
            IPhraseProvider phraseProvider,
            IObjectDrawer canvasDrawer,
            IObjectDrawer dotDarwer,
            IObjectDrawer verticalLineDrawer,
            IObjectDrawer horizontalLineDrawer,
            IObjectDrawer gooseDrawer,
            IBoard board)
        {
            this.drawManager = drawManager;
            this.keyboardManager = keyboardManager;
            this.phraseProvider = phraseProvider;
            this.canvasDrawer = canvasDrawer;
            this.dotDrawer = dotDarwer;
            this.verticalLineDrawer = verticalLineDrawer;
            this.horizontalLineDrawer = horizontalLineDrawer;
            this.gooseDrawer = gooseDrawer;
            this.board = board;
        }

        public void Run()
        {
            ShowMenu();

            DrawDelegate canvasDelegate = new DrawDelegate(canvasDrawer.DrawObject);
            DrawDelegate dotDelegate = new DrawDelegate(dotDrawer.DrawObject);
            DrawDelegate verticalLineDelegate = new DrawDelegate(verticalLineDrawer.DrawObject);
            DrawDelegate horizontalLineDelegate = new DrawDelegate(horizontalLineDrawer.DrawObject);
            DrawDelegate gooseDelegate = new DrawDelegate(gooseDrawer.DrawObject);

            do
            {
                consoleKeyPressed = keyboardManager.ReadKey();

                switch (consoleKeyPressed.Key)
                {
                    case ConsoleKey.D1:
                        drawingDelegates += canvasDelegate;
                        break;

                    case ConsoleKey.D2:
                        drawingDelegates += dotDelegate;
                        break;

                    case ConsoleKey.D3:
                        drawingDelegates += verticalLineDelegate;
                        break;

                    case ConsoleKey.D4:
                        drawingDelegates += horizontalLineDelegate;
                        break;
                    case ConsoleKey.D5:
                        drawingDelegates += gooseDelegate;
                        break;


                    case ConsoleKey.Q:
                        drawingDelegates -= canvasDelegate;
                        break;

                    case ConsoleKey.W:
                        drawingDelegates -= dotDelegate;
                        break;

                    case ConsoleKey.E:
                        drawingDelegates -= verticalLineDelegate;
                        break;

                    case ConsoleKey.R:
                        drawingDelegates -= horizontalLineDelegate;
                        break;
                    case ConsoleKey.T:
                        drawingDelegates -= gooseDelegate;
                        break;

                    case ConsoleKey.Escape:
                        continue;
                    default:
                        ShowMenu();
                        continue;
                }

                drawManager.Draw(drawingDelegates, board);
            }
            while (consoleKeyPressed.Key != ConsoleKey.Escape);
        }

        private void ShowMenu()
        {
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.Welcome));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.CanvasDrawMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.DotDrawMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.VerticalDrawMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.HorizontalDrawMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.GooseDrawMessage));
            drawManager.WriteLine(string.Empty);
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.CanvasEraseMesage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.DotEraseMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.VerticalEraseMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.HorizontalEraseMessage));
            drawManager.WriteLine(phraseProvider.GetPhrase(Phrase.GooseEraseMessage));
        }
    }
}