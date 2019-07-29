//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ConsoleDrawGame
{
    using Interfaces;

    /// <summary>
    /// Main game class
    /// </summary>
    internal class Game
    {
        /// <summary>
        /// Field for get game options
        /// </summary>
        private readonly ISettingsProvider settingsProvider;

        /// <summary>
        /// Field for input output info
        /// </summary>
        private readonly IInputOutput inputOutputComponent;

        /// <summary>
        /// Field with messages for the game
        /// </summary>
        private readonly IPhraseProvider phraseProvider;

        /// <summary>
        /// Field to screen
        /// </summary>
        private readonly IBoard board;

        /// <summary>
        /// Field for drawing
        /// </summary>
        private readonly IFigureProvider figureProvider;

        /// <summary>
        /// Field with game options
        /// </summary>
        private readonly GameSettings gameSettings;

        /// <summary>
        /// Initializes a new instance of the<see cref="Game" /> class
        /// </summary>
        /// <param name="settingsProvider">Setting interface</param>
        /// <param name="inputOutputDevice">Read/Write Interface</param>
        /// <param name="phraseProvider">Get messages interface</param>
        /// <param name="board">Screen for drawing interface</param>
        /// <param name="figureProvider">Drawing figures interface</param>
        public Game(
            ISettingsProvider settingsProvider,
            IInputOutput inputOutputDevice,
            IPhraseProvider phraseProvider,
            IBoard board,
            IFigureProvider figureProvider)
        {
            this.settingsProvider = settingsProvider;
            this.inputOutputComponent = inputOutputDevice;
            this.phraseProvider = phraseProvider;
            this.board = board;
            this.figureProvider = figureProvider;

            this.gameSettings = this.settingsProvider.GameSettings();

            this.board.BoardSizeX = this.gameSettings.Length;
            this.board.BoardSizeY = this.gameSettings.Width;
        }

        /// <summary>
        /// For drawing figures
        /// </summary>
        /// <param name="board">Screen for drawing</param>
        private delegate void DrawFigure(IBoard board);

        /// <summary>
        /// Main game method 
        /// </summary>
        public void Run()
        {
            bool drawnDot = false, drawnHLine = false, drawnVLine = false, drawnCurve = false;
            DrawFigure draw = delegate (IBoard board) { };
            string inputString = string.Empty;

            while (inputString.ToLower() != this.gameSettings.ExitCode.ToLower())
            {
                this.inputOutputComponent.Clear();
                this.board.Draw();
                draw(this.board);

                this.inputOutputComponent.CursorPosition(0, this.board.BoardSizeY + 1);
                this.inputOutputComponent.WriteOutputLine(this.phraseProvider.GetPhrase("Welcome"));
                this.inputOutputComponent.WriteOutputLine(this.phraseProvider.GetPhrase("Exit") + this.gameSettings.ExitCode);
                inputString = this.inputOutputComponent.ReadInputLine();

                switch (inputString)
                {
                    case "1":
                       draw = (drawnDot == false) ?
                            draw + this.figureProvider.Dot :
                                draw - this.figureProvider.Dot;
                       drawnDot = !drawnDot;
                       break;
                    case "2":
                        draw = (drawnHLine == false) ?
                             draw + this.figureProvider.HorizontalLine :
                                 draw - this.figureProvider.HorizontalLine;
                        drawnHLine = !drawnHLine;
                        break;
                    case "3":
                        draw = (drawnVLine == false) ?
                            draw + this.figureProvider.VerticalLine :
                                draw - this.figureProvider.VerticalLine;
                        drawnVLine = !drawnVLine;
                        break;
                    case "4":
                        draw = (drawnCurve == false) ?
                            draw + this.figureProvider.Rectangle :
                                draw - this.figureProvider.Rectangle;
                        drawnCurve = !drawnCurve;
                        break;
                    case "5":
                        this.SetBoardLengthSize();
                        break;
                    case "6":
                        this.SetBoardWidthSize();
                        break;
                    default:
                        break;
                }
            }

            this.inputOutputComponent.WriteOutputLine(this.phraseProvider.GetPhrase("End"));
            this.inputOutputComponent.ReadInputKey();
        }

        /// <summary>
        /// Check value to resize board
        /// </summary>
        /// <param name="size">Value to resize</param>
        /// <param name="maxSize">Value to max resize</param>
        /// <returns>Operation result</returns>
        private bool CheckBoardSize(int size, int maxSize)
        {
            if (size >= 6 && size <= maxSize)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Change width`s board
        /// </summary>
        private void SetBoardLengthSize()
        {
            this.inputOutputComponent.WriteOutputLine(this.phraseProvider.GetPhrase("EnterL") + this.gameSettings.MaxLength);
            string resultStr = this.inputOutputComponent.ReadInputLine();

            bool success = int.TryParse(resultStr, out int number);
            if (success)
            {
                if (this.CheckBoardSize(number, this.gameSettings.MaxLength))
                {
                    this.board.BoardSizeX = number;
                    return;
                }
            }

            this.inputOutputComponent.WriteOutputLine(this.phraseProvider.GetPhrase("BadValue"));
            this.inputOutputComponent.ReadInputKey();
        }

        /// <summary>
        /// Change width`s board
        /// </summary>
        private void SetBoardWidthSize()
        {
            this.inputOutputComponent.WriteOutputLine(this.phraseProvider.GetPhrase("EnterW") + this.gameSettings.MaxWidth);
            string resultStr = this.inputOutputComponent.ReadInputLine();

            bool success = int.TryParse(resultStr, out int number);
            if (success)
            {
                if (this.CheckBoardSize(number, this.gameSettings.MaxWidth))
                {
                    this.board.BoardSizeY = number;
                    return;
                }
            }

            this.inputOutputComponent.WriteOutputLine(this.phraseProvider.GetPhrase("BadValue"));
            this.inputOutputComponent.ReadInputKey();
        }
    }
}