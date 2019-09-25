// <copyright file="Game.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw
{
    using ConsoleDraw.Interfaces;

    /// <summary>
    /// Game.cs.
    /// </summary>
    public class Game
    {
        private readonly ISettingsProvider settingsProvider;
        private readonly IInputOutputDevice iOProvoder;
        private readonly IPhraseProvider phraseProvider;
        private readonly IBoard board;
        private readonly IFigureProvider figureProvider;
        private readonly GameSettings gameSettings;

        private bool dotFlag;
        private bool horizontalLineFlag;
        private bool verticalLineFlag;
        private bool curveFlag;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="settingsProvider">setting provider.</param>
        /// <param name="iOProvider">io provider.</param>
        /// <param name="phraseProvider">phrase provider.</param>
        /// <param name="board">board provider.</param>
        /// <param name="figureProvider">figure provider.</param>
        public Game(
            ISettingsProvider settingsProvider,
            IInputOutputDevice iOProvider,
            IPhraseProvider phraseProvider,
            IBoard board,
            IFigureProvider figureProvider)
        {
            this.settingsProvider = settingsProvider;
            this.iOProvoder = iOProvider;
            this.phraseProvider = phraseProvider;
            this.board = board;
            this.figureProvider = figureProvider;

            this.gameSettings = this.settingsProvider.GetGameSettings();
        }

        private delegate void Draw(IBoard board);

        /// <summary>
        /// start game.
        /// </summary>
        public void Start()
        {
            this.phraseProvider.SetLanguage(this.gameSettings.Language);

            this.board.BoardSizeX = this.gameSettings.Length;
            this.board.BoardSizeY = this.gameSettings.Width;

            Draw draw = board => { };

            this.board.Create();
            string enter = string.Empty;

            while (enter != null && !enter.Equals(this.gameSettings.ExitCode))
            {
                this.iOProvoder.SetPosition(0, this.gameSettings.Width);
                this.iOProvoder.WriteLineOutput(this.phraseProvider.GetPhrase("Description"));
                this.iOProvoder.WriteLineOutput(this.phraseProvider.GetPhrase("Enter"));
                this.iOProvoder.WriteOutput(this.phraseProvider.GetPhrase("Exit"));
                this.iOProvoder.WriteLineOutput(this.gameSettings.ExitCode);

                enter = this.iOProvoder.ReadInput();

                switch (enter)
                {
                    case "1":
                        if (this.dotFlag)
                        {
                            draw -= this.figureProvider.Dot;
                        }
                        else
                        {
                            draw += this.figureProvider.Dot;
                        }

                        this.dotFlag = !this.dotFlag;
                        break;
                    case "2":
                        if (this.horizontalLineFlag)
                        {
                            draw -= this.figureProvider.HorizontalLine;
                        }
                        else
                        {
                            draw += this.figureProvider.HorizontalLine;
                        }

                        this.horizontalLineFlag = !this.horizontalLineFlag;
                        break;
                    case "3":
                        if (this.verticalLineFlag)
                        {
                            draw -= this.figureProvider.VerticalLine;
                        }
                        else
                        {
                            draw += this.figureProvider.VerticalLine;
                        }

                        this.verticalLineFlag = !this.verticalLineFlag;
                        break;
                    case "4":
                        if (this.curveFlag)
                        {
                            draw -= this.figureProvider.Curve;
                        }
                        else
                        {
                            draw += this.figureProvider.Curve;
                        }

                        this.curveFlag = !this.curveFlag;
                        break;
                    default:
                        break;
                }

                this.iOProvoder.Clear();
                this.board.Create();
                draw(this.board);
            }
        }
    }
}
