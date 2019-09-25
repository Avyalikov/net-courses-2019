namespace ConsoleCanvas
{
    using System;
    using ConsoleCanvas.Interfaces;

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
            this.ShowMenu();

            DrawDelegate canvasDelegate = new DrawDelegate(this.canvasDrawer.DrawObject);
            DrawDelegate dotDelegate = new DrawDelegate(this.dotDrawer.DrawObject);
            DrawDelegate verticalLineDelegate = new DrawDelegate(this.verticalLineDrawer.DrawObject);
            DrawDelegate horizontalLineDelegate = new DrawDelegate(this.horizontalLineDrawer.DrawObject);
            DrawDelegate gooseDelegate = new DrawDelegate(this.gooseDrawer.DrawObject);

            do
            {
                this.consoleKeyPressed = this.keyboardManager.ReadKey();

                switch (this.consoleKeyPressed.Key)
                {
                    case ConsoleKey.D1:
                        this.drawingDelegates += canvasDelegate;
                        break;

                    case ConsoleKey.D2:
                        this.drawingDelegates += dotDelegate;
                        break;

                    case ConsoleKey.D3:
                        this.drawingDelegates += verticalLineDelegate;
                        break;

                    case ConsoleKey.D4:
                        this.drawingDelegates += horizontalLineDelegate;
                        break;
                    case ConsoleKey.D5:
                        this.drawingDelegates += gooseDelegate;
                        break;

                    case ConsoleKey.Q:
                        this.drawingDelegates -= canvasDelegate;
                        break;

                    case ConsoleKey.W:
                        this.drawingDelegates -= dotDelegate;
                        break;

                    case ConsoleKey.E:
                        this.drawingDelegates -= verticalLineDelegate;
                        break;

                    case ConsoleKey.R:
                        this.drawingDelegates -= horizontalLineDelegate;
                        break;
                    case ConsoleKey.T:
                        this.drawingDelegates -= gooseDelegate;
                        break;

                    case ConsoleKey.Escape:
                        continue;
                    default:
                        this.ShowMenu();
                        continue;
                }

                this.drawManager.Draw(this.drawingDelegates, this.board);
            }
            while (this.consoleKeyPressed.Key != ConsoleKey.Escape);
        }

        private void ShowMenu()
        {
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.Welcome));
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.CanvasDrawMessage));
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.DotDrawMessage));
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.VerticalDrawMessage));
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.HorizontalDrawMessage));
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.GooseDrawMessage));
            this.drawManager.WriteLine(string.Empty);
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.CanvasEraseMesage));
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.DotEraseMessage));
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.VerticalEraseMessage));
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.HorizontalEraseMessage));
            this.drawManager.WriteLine(this.phraseProvider.GetPhrase(Phrase.GooseEraseMessage));
        }
    }
}