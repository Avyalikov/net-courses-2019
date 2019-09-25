//-----------------------------------------------------------------------
// <copyright file="GameMenu.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DashboardGame
{
    using static PhraseTypes;

    /// <summary>
    /// This class contains logic for game menu.
    /// </summary>
    internal class GameMenu
    {
        /// <summary>
        /// Initializes a new instance of the GameMenu class.
        /// </summary>
        /// <param name="board">It is an IBoard, where menu should be printed.</param>
        /// <param name="phraseProvider">IPhraseProvider which is used for printing messages.</param>
        public GameMenu(IBoard board, IPhraseProvider phraseProvider)
        {
            this.PhraseProvider = phraseProvider;
            this.Board = board;
        }

        /// <summary>
        /// This delegate is used for interacting with plotting methods.
        /// </summary>
        /// <param name="board">IBoard for drawing plots.</param>
        public delegate void Draw(IBoard board);

        /// <summary>
        /// Gets or sets methods for drawing different plots.
        /// </summary>
        public Draw DrawFigures { get; set; }

        /// <summary>
        /// Gets IPhraseProvider implementation which is used for printing messages.
        /// </summary>
        public IPhraseProvider PhraseProvider { get; }

        /// <summary>
        /// Gets IBoard implementation which is used for printing messages.
        /// </summary>
        private IBoard Board { get; }

        /// <summary>
        /// This method prints info about the game.
        /// </summary>
        public void ShowInfo()
        {
            this.Board.DrawAtPosition(-15, 10, this.PhraseProvider.GetPhrase(Welcome));
            this.Board.DrawAtPosition(-20, 9, this.PhraseProvider.GetPhrase(Options));
            this.Board.DrawAtPosition(-20, 8, $"1 - { this.PhraseProvider.GetPhrase(DotOption) }");
            this.Board.DrawAtPosition(-20, 7, $"2 - { this.PhraseProvider.GetPhrase(VerLineOption) }");
            this.Board.DrawAtPosition(-20, 6, $"3 - { this.PhraseProvider.GetPhrase(HorLineOption) }");
            this.Board.DrawAtPosition(-20, 5, $"4 - { this.PhraseProvider.GetPhrase(ParOption) }");
            this.Board.DrawAtPosition(-25, 4, this.PhraseProvider.GetPhrase(HowToChoose));
            this.Board.DrawAtPosition(-25, 3, this.PhraseProvider.GetPhrase(Example1));
            this.Board.DrawAtPosition(-25, 2, this.PhraseProvider.GetPhrase(Example2));
            this.Board.DrawAtPosition(-25, 1, this.PhraseProvider.GetPhrase(Escape));
            this.Board.DrawAtPosition(-25, 0, this.PhraseProvider.GetPhrase(Choose));
        }

        /// <summary>
        /// This method gets User's input from IBoard.
        /// </summary>
        /// <returns>Returns a string with input.</returns>
        public string GetUserChoice()
        {
            this.Board.DrawAtPosition(-6, 1, string.Empty);
            return this.Board.ReadLine();
        }

        /// <summary>
        /// This method get user choice, parses it and check if the data from input is enough for plotting.
        /// </summary>
        /// <param name="choice">String with user's input, where the choice should look like "124".</param>
        /// <returns>True if plotting can be done, false otherwise.</returns>
        public bool ParseUserChoice(string choice)
        {
            this.Board.Clear();
            bool isCorrect = false;
            int y = 9;
            if (choice != string.Empty)
            {
                this.Board.DrawAtPosition(-20, y--, this.PhraseProvider.GetPhrase(ChoiceIs) + choice);

                if (choice.Contains("2"))
                {
                    this.Board.DrawAtPosition(-20, y--, $"2 - { this.PhraseProvider.GetPhrase(VerLineOption) }");
                    this.DrawFigures += Drawer.DrawVerticalLine;
                    isCorrect = true;
                }

                if (choice.Contains("3"))
                {
                    this.Board.DrawAtPosition(-20, y--, $"3 - { this.PhraseProvider.GetPhrase(HorLineOption) }");
                    this.DrawFigures += Drawer.DrawHorizontalLine;
                    isCorrect = true;
                }

                if (choice.Contains("4"))
                {
                    this.Board.DrawAtPosition(-20, y--, $"4 - { this.PhraseProvider.GetPhrase(ParOption) }");
                    this.DrawFigures += Drawer.DrawParabola;
                    isCorrect = true;
                }

                if (choice.Contains("1"))
                {
                    this.Board.DrawAtPosition(-20, y--, $"1 - { this.PhraseProvider.GetPhrase(DotOption) }");

                    this.DrawFigures += Drawer.DrawPoint;
                    isCorrect = true;
                }

                if (isCorrect)
                {
                    this.Board.DrawAtPosition(-20, y--, this.PhraseProvider.GetPhrase(TypeToPlot));
                    this.Board.ReadKey();
                }

                if (!isCorrect)
                {
                    this.Board.DrawAtPosition(-25, y--, this.PhraseProvider.GetPhrase(IncorrectInput1));
                    this.Board.DrawAtPosition(-25, y--, this.PhraseProvider.GetPhrase(IncorrectInput2));
                }
            }

            if (!isCorrect && choice == string.Empty)
            {
                this.Board.DrawAtPosition(-25, y--, this.PhraseProvider.GetPhrase(EmptyInput));
                this.Board.DrawAtPosition(-25, y--, this.PhraseProvider.GetPhrase(IncorrectInput2));
            }

            return isCorrect;
        }
    }
}
