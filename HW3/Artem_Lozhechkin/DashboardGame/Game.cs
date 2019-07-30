//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DashboardGame
{
    using System;

    /// <summary>
    /// This class contains Game app logic.
    /// </summary>
    internal class Game
    {
        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        /// <param name="board">IBoard implementation where the game must be run.</param>
        /// <param name="menu">GameMenu class which is used for controlling gameplay.</param>
        public Game(IBoard board, GameMenu menu)
        {
            this.Board = board;
            this.Menu = menu;
        }

        /// <summary>
        /// Gets IBoard implementation where the game must be run.
        /// </summary>
        private IBoard Board { get; }

        /// <summary>
        /// Gets GameMenu implementation which is used for controlling gameplay.
        /// </summary>
        private GameMenu Menu { get; }

        /// <summary>
        /// This methods starts the game.
        /// </summary>
        public void Play()
        {
            string userInput;
            do
            {
                this.Board.Clear();
                this.Menu.ShowInfo();

                do
                {
                    userInput = this.Board.ReadLine();
                }
                while (!this.Menu.ParseUserChoice(userInput));

                this.Board.Clear();
                this.Board.DrawAxis();
                this.Menu.DrawFigures(this.Board);
                this.Menu.DrawFigures = null;
            }
            while (this.Board.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
