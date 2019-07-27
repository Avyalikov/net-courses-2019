// <copyright file="Game.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    using System;

    /// <summary>
    /// Basic game logic class
    /// </summary>
    internal class Game
    {
        /// <summary>
        /// Provides phrases from a file
        /// </summary>
        private readonly IPhraseProvider phraseProvider;

        /// <summary>
        /// Provides console IO device
        /// </summary>
        private readonly IInputOutputDevice inputOutputDevice;

        /// <summary>
        /// Draws in console
        /// </summary>
        private readonly IDrawOnBoard drawOnBoard;

        /// <summary>
        /// Basic settings
        /// </summary>
        private readonly GameSettings gameSettings;

        /// <summary>
        /// Defines board
        /// </summary>
        private readonly IBoard board;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="phraseProvider">The phraseProvider<see cref="IPhraseProvider"/></param>
        /// <param name="inputOutputDevice">The inputOutputDevice<see cref="IInputOutputDevice"/></param>
        /// <param name="settingsProvider">The settingsProvider<see cref="ISettingsProvider"/></param>
        /// <param name="board">The board<see cref="IBoard"/></param>
        /// <param name="drawOnBoard">The drawOnBoard<see cref="IDrawOnBoard"/></param>
        public Game(
            IPhraseProvider phraseProvider,
            IInputOutputDevice inputOutputDevice,
            ISettingsProvider settingsProvider,
            IBoard board,
            IDrawOnBoard drawOnBoard)
        {
            this.phraseProvider = phraseProvider;
            this.inputOutputDevice = inputOutputDevice;
            this.gameSettings = settingsProvider.GetGameSettings();
            this.board = board;
            this.drawOnBoard = drawOnBoard;
        }

        /// <summary>
        /// The Draw
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        private delegate void Draw(IBoard board);

        /// <summary>
        /// Basic game logic method
        /// </summary>
        internal void Run()
        {
            this.inputOutputDevice.Print(this.phraseProvider.GetPhrase("Welcome"));
            this.inputOutputDevice.Print(this.phraseProvider.GetPhrase("ChooseBoardSize"));
            int boardSizeX, boardSizeY;
            this.inputOutputDevice.Print(this.phraseProvider.GetPhrase("ChooseHorizontalLength"));
            int.TryParse(this.inputOutputDevice.InputValue(), out boardSizeX);
            this.board.BoardSizeX = boardSizeX;
            this.inputOutputDevice.Print(this.phraseProvider.GetPhrase("ChooseVerticalHeight"));
            int.TryParse(this.inputOutputDevice.InputValue(), out boardSizeY);
            this.board.BoardSizeY = boardSizeY;
            this.inputOutputDevice.Print(this.phraseProvider.GetPhrase("ExitCode") + this.gameSettings.ExitCode);
            string userInput;
            Draw draw = new Draw(this.drawOnBoard.ClearConsole);
            draw += this.drawOnBoard.DrawBoard;
            while (true)
            {
                this.inputOutputDevice.Print("\n" + this.phraseProvider.GetPhrase("Draw") + this.gameSettings.ClearBoard);
                userInput = this.inputOutputDevice.InputValue();
                if (userInput.ToLowerInvariant() == this.gameSettings.ExitCode.ToLowerInvariant())
                {
                    break;
                }

                if (userInput.ToLowerInvariant() == this.gameSettings.FirstFigure.ToLowerInvariant())
                {
                    draw += this.drawOnBoard.DrawSimpleDot;
                }

                if (userInput.ToLowerInvariant() == this.gameSettings.SecondFigure.ToLowerInvariant())
                {
                    draw += this.drawOnBoard.DrawVerticalLine;
                }

                if (userInput.ToLowerInvariant() == this.gameSettings.Third.ToLowerInvariant())
                {
                    draw += this.drawOnBoard.DrawHorizontalLine;
                }

                if (userInput.ToLowerInvariant() == this.gameSettings.ForthFigure.ToLowerInvariant())
                {
                    draw += this.drawOnBoard.DrawV;
                }

                if (userInput.ToLowerInvariant() == this.gameSettings.ClearBoard.ToLowerInvariant())
                {
                    draw = new Draw(this.drawOnBoard.ClearConsole);
                    draw += this.drawOnBoard.DrawBoard;
                }

                draw(this.board);
            }

            this.inputOutputDevice.Print(this.phraseProvider.GetPhrase("ThankYouForPlaying"));
        }
    }
}
