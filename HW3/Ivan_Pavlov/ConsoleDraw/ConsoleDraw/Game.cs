namespace ConsoleDraw
{
    using ConsoleDraw.Interfaces;

    public class Game
    {
        private readonly ISettingsProvider settingsProvider;
        private readonly IInputOutputDevice IOProvoder;
        private readonly IPhraseProvider phraseProvider;
        private readonly IBoard board;
        private readonly IFigureProvider figureProvider;
        private readonly GameSettings gameSettings;

        private bool dotFlag;
        private bool horizontalLineFlag;
        private bool verticalLineFlag;
        private bool curveFlag;

        private delegate void Draw(IBoard board);

        public Game(
            ISettingsProvider settingsProvider,
            IInputOutputDevice IOProvider,
            IPhraseProvider phraseProvider,
            IBoard board,
            IFigureProvider figureProvider)
        {
            this.settingsProvider = settingsProvider;
            this.IOProvoder = IOProvider;
            this.phraseProvider = phraseProvider;
            this.board = board;
            this.figureProvider = figureProvider;

            this.gameSettings = this.settingsProvider.GetGameSettings();
        }       

        public void Start()
        {
            this.phraseProvider.SetLanguage(this.gameSettings.Language);

            this.board.BoardSizeX = this.gameSettings.Length;
            this.board.BoardSizeY = this.gameSettings.Width;

            Draw draw = delegate (IBoard board) { };

            this.board.Create();
            string enter = string.Empty;

            while(enter != null && !enter.Equals(this.gameSettings.ExitCode))
            {
                this.IOProvoder.SetPosition(0, this.gameSettings.Width);
                this.IOProvoder.WriteLineOutput(this.phraseProvider.GetPhrase("Description"));
                this.IOProvoder.WriteLineOutput(this.phraseProvider.GetPhrase("Enter"));
                this.IOProvoder.WriteOutput(this.phraseProvider.GetPhrase("Exit"));
                this.IOProvoder.WriteLineOutput(this.gameSettings.ExitCode);

                enter = this.IOProvoder.ReadInput();

                switch (enter)
                {
                    case "1":
                        if (this.dotFlag) draw -= this.figureProvider.Dot;
                        else draw += this.figureProvider.Dot;
                        this.dotFlag = !this.dotFlag;
                        break;
                    case "2":
                        if (this.horizontalLineFlag) draw -= this.figureProvider.HorizontalLine;
                        else draw += this.figureProvider.HorizontalLine;
                        this.horizontalLineFlag = !this.horizontalLineFlag;
                        break;
                    case "3":
                        if (this.verticalLineFlag) draw -= this.figureProvider.VerticalLine;
                        else draw += this.figureProvider.VerticalLine;
                        this.verticalLineFlag = !this.verticalLineFlag;
                        break;
                    case "4":
                        if (this.curveFlag) draw -= this.figureProvider.Curve;
                        else draw += this.figureProvider.Curve;
                        this.curveFlag = !this.curveFlag;
                        break;
                    default:
                        break;
                }

                this.IOProvoder.Clear();
                this.board.Create();
                draw(this.board);
            }
        }
    }
}
