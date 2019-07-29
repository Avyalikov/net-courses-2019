//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game
{
    using System;
    using System.Collections.Generic;
    using Components.DataTypes;
    using Interfaces;
    
    /// <summary>
    /// Main game class
    /// </summary>
    public class Game
    {
        /// <summary>
        /// GameSettings field
        /// </summary>
        private readonly GameSettings st;

        /// <summary>
        /// IInputOutput field
        /// </summary>
        private readonly IInputOutput io;

        /// <summary>
        /// IPhraseProvider field
        /// </summary>
        private readonly IPhraseProvider phraseProvider;

        /// <summary>
        /// IFigureManager field
        /// </summary>
        private readonly IFigureManager figureManager;

        /// <summary>
        /// IBoard field
        /// </summary>
        private readonly IBoard board;

        /// <summary>
        /// List of chosen figure
        /// </summary>
        private List<string> chosenFigure = new List<string>();

        /// <summary>
        /// Draw figures delegate field
        /// </summary>
        private Draw draw;

        /// <summary>
        /// Currently chosen figure
        /// </summary>
        private string curFigure;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game" /> class
        /// </summary>
        /// <param name="st">Game settings object</param>
        /// <param name="board">Board  component</param>
        /// <param name="io">Input/Output component</param>
        /// <param name="phraseProvider">Phrase provider component</param>
        /// <param name="figureManager">Figure manager component</param>
        public Game(
            GameSettings st,
            IBoard board,
            IInputOutput io,
            IPhraseProvider phraseProvider,
            IFigureManager figureManager)
        {
            this.st = st;
            this.board = board;
            this.io = io;
            this.phraseProvider = phraseProvider;
            this.figureManager = figureManager;
        }

        /// <summary>
        /// Draw figures delegate
        /// </summary>
        /// <param name="board">Board  component</param>
        private delegate void Draw(IBoard board);

        /// <summary>
        /// Main game method
        /// </summary>
        public void Run()
        {
            bool isFirst = true;
            this.draw += this.board.Draw;
            while (true)
            {
                this.io.Clear();
                this.draw?.Invoke(this.board);

                this.io.SetCursorPosition(0, this.st.BorderHeight + 2);
                this.io.Print(this.phraseProvider.GetPhrase(Phrase.Welcome) + Environment.NewLine);
                if (!isFirst)
                {
                    this.io.Print(this.phraseProvider.GetPhrase(Phrase.YourChosenFigure), end: string.Empty);
                    this.io.Print(this.chosenFigure);                    
                }

                isFirst = false;

                this.curFigure = this.GetFigure();
                this.chosenFigure.Add(this.curFigure);
                switch (this.curFigure)
                {
                    case "1":
                        this.draw += this.figureManager.DrawFirst;
                        break;
                    case "2":
                        this.draw += this.figureManager.DrawSecond;
                        break;
                    case "3":
                        this.draw += this.figureManager.DrawThird;
                        break;
                    case "4":
                        this.draw += this.figureManager.DrawFourth;
                        break;
                    case "exit":
                        return;
                }                
            }
        }

        /// <summary>
        /// User input method
        /// </summary>
        /// <returns>Return user's typed integer</returns>
        private string GetFigure()
        {
            string str;

            while (true)
            {
                this.io.Print(this.phraseProvider.GetPhrase(Phrase.Choose), end: string.Empty);
                str = this.io.Input();
                switch (str.ToLower())
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "exit":
                        return str;
                }

                this.io.Print(this.phraseProvider.GetPhrase(Phrase.WrongValue));
            }
        }
    }
}