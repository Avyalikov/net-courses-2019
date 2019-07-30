namespace Console_Draw_Game
{
    using System;

    class Game
    {
        private readonly Interfaces.IBoard board;
        private readonly Interfaces.IFigures figures;
        private readonly Interfaces.IInputOutputModule ioModule;
        private readonly Interfaces.IPhraseProvider phraseProvider;
        private readonly Interfaces.ISettingsProvider settingsProvider;

        private bool dotFlag;
        private bool horizontalLineFlag;
        private bool verticalLineFlag;
        private bool sharpLineFlag;

        public Game(Interfaces.IBoard board,
            Interfaces.IFigures figures,
            Interfaces.IInputOutputModule ioModule,
            Interfaces.IPhraseProvider phraseProvider,
            Interfaces.ISettingsProvider settingsProvider)
        {
            this.board = board;
            this.figures = figures;
            this.ioModule = ioModule;
            this.phraseProvider = phraseProvider;
            this.settingsProvider = settingsProvider;
        }

        delegate void Draw(Interfaces.IBoard board);

        public void Start()
        {
            settingsProvider.ParseXML();
            phraseProvider.ParseXML(settingsProvider.GetSetting("Language")); //Get language from settings file

            int boardLength = Convert.ToInt32(settingsProvider.GetSetting("Length")); //Get lenght of dashboard from settings file
            int boardWidth = Convert.ToInt32(settingsProvider.GetSetting("Width")); //Get width of dashboard from settings file

            this.board.BoardSizeX = boardLength;
            this.board.BoardSizeY = boardWidth;

            Draw draw = delegate (Interfaces.IBoard board) { };

            this.board.Create();
            string userInput = string.Empty;

            while (userInput != null && !userInput.ToLower().Equals("exit"))
            {
                this.ioModule.SetPosition(0, boardWidth);

                ioModule.WriteOutput(phraseProvider.GetMessage("Start") + "\n");
                ioModule.WriteOutput(phraseProvider.GetMessage("Rules") + "\n");
                ioModule.WriteOutput(phraseProvider.GetMessage("ExitCommand") + "\n");

                userInput = ioModule.ReadInput();

                switch (userInput)
                {
                    case "1":
                        if (this.dotFlag)
                        {
                            draw -= this.figures.Dot;
                        }

                        else
                        {
                            draw += this.figures.Dot;
                        }

                        this.dotFlag = !this.dotFlag;
                        break;

                    case "2":
                        if (this.horizontalLineFlag)
                        {
                            draw -= this.figures.HorizontalLine;
                        }

                        else
                        {
                            draw += this.figures.HorizontalLine;
                        }

                        this.horizontalLineFlag = !this.horizontalLineFlag;
                        break;

                    case "3":
                        if (this.verticalLineFlag)
                        {
                            draw -= this.figures.VerticalLine;
                        }
                        else
                        {
                            draw += this.figures.VerticalLine;
                        }

                        this.verticalLineFlag = !this.verticalLineFlag;
                        break;

                    case "4":
                        if (this.sharpLineFlag)
                        {
                            draw -= this.figures.SharpLine;
                        }
                        else
                        {
                            draw += this.figures.SharpLine;
                        }

                        this.sharpLineFlag = !this.sharpLineFlag;
                        break;

                    default:
                        break;
                }

                this.ioModule.Clear();
                this.board.Create();
                draw(this.board);
            }
        }
    }
}
