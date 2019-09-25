// <copyright file="Game.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    using System;

    /// <summary>
    /// Main game class
    /// </summary>
    public class Game
    {
        /// <summary>
        /// IBoard field
        /// </summary>
        private readonly IBoard board;

        /// <summary>
        /// IPhraseProvider field
        /// </summary>
        private readonly IPhraseProvider phraseProvider;

        /// <summary>
        /// IInputOutputDevice field
        /// </summary>
        private readonly IInputOutputDevice inputOutputDevice;

        /// <summary>
        /// IDrawOnBoard field
        /// </summary>
        private readonly IDrawOnBoard drawOnBoard;

        /// <summary>
        /// ISettingsProvider field
        /// </summary>
        private readonly ISettingsProvider settingsProvider;

        /// <summary>
        /// GameSettings field
        /// </summary>
        private readonly GameSettings gameSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="board">Board component</param>
        /// <param name="phraseProvider">Phrase Provider component</param>
        /// <param name="inputOutputDevice">Input and output device component</param>
        /// <param name="drawOnBoard">Draw on board component</param>
        /// <param name="settingsProvider">Settings provider component</param>
        public Game(
            IBoard board,
            IPhraseProvider phraseProvider,
            IInputOutputDevice inputOutputDevice,
            IDrawOnBoard drawOnBoard,
            ISettingsProvider settingsProvider)
        {
            this.board = board;
            this.phraseProvider = phraseProvider;
            this.inputOutputDevice = inputOutputDevice;
            this.drawOnBoard = drawOnBoard;
            this.settingsProvider = settingsProvider;

            this.gameSettings = settingsProvider.GetGameSettings();
        }

        /// <summary>
        /// Draw figures delegate
        /// </summary>
        /// <param name="board">Board component</param>
        private delegate void Draw(IBoard board);

        /// <summary>
        /// Basic game method
        /// </summary>
        public void Run()
        {
            this.inputOutputDevice.WriteOutput(this.phraseProvider.GetPhrase("Welcome"));
            this.board.boardSizeX = this.gameSettings.Length;
            this.board.boardSizeY = this.gameSettings.Width;
            int figure;
            Draw draw = new Draw(this.drawOnBoard.CleanBoard);
            draw += this.board.DrawBoard;
            while (true)
            {
                this.inputOutputDevice.WriteOutput(this.phraseProvider.GetPhrase("TheFiguresAre"));
                this.inputOutputDevice.WriteOutput(this.phraseProvider.GetPhrase("ChooseYourFigure"));

                figure = this.UserInput();
                //if it is negative, than input data is exit code
                if (figure < 0)
                {
                    break;
                }

                if (figure == 1)
                {
                    draw += this.drawOnBoard.DrawSimpleDot;
                }

                if (figure == 2)
                {
                    draw += this.drawOnBoard.DrawHorizontalLine;
                }

                if (figure == 3)
                {
                    draw += this.drawOnBoard.DrawVerticalLine;
                }

                if (figure == 4)
                {
                    draw += this.drawOnBoard.DrawCircle;
                }

                draw(this.board);
                Console.SetCursorPosition(0, this.board.boardSizeY + 1);
            }
        }

        /// <summary>
        /// User input method
        /// </summary>
        /// <returns>Entered number</returns>
        private int UserInput()
        {
            int result = -1;
            int enteredNumber;
            do
            {
                var input = this.inputOutputDevice.ReadInput();
                //if input data is number
                if (int.TryParse(input, out enteredNumber))
                {
                    result = 1;
                }
                //if input data is equally to exit code
                else if (input.ToLowerInvariant().Equals(this.gameSettings.ExitCode))
                {
                    this.inputOutputDevice.WriteOutput(this.phraseProvider.GetPhrase("ThankYouForPlaying"));
                    result = 0;
                }
                //if input data is string, but not exit code
                else
                {
                    this.inputOutputDevice.WriteOutput(this.phraseProvider.GetPhrase("ItIsNotANumber"));
                }
            }
            while (result < 0); //while input data is not a number and exit code

            //if it is exit code, than return negative number
            if (result == 0)
            {
                enteredNumber = -1;
            }

            return enteredNumber;
        }
    }
}
