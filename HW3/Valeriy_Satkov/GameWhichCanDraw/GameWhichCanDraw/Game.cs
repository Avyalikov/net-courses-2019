// <copyright file="Game.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw
{
    using System;
    using System.Collections.Generic;
    using GameWhichCanDraw.Interfaces;

    /// <summary>
    /// Basic game logic class
    /// </summary>
    internal class Game
    {
        /// <summary>
        /// Provides settings from a file
        /// </summary>
        private readonly ISettingsProvider settingsProvider;

        /// <summary>
        /// Provides Input-Output device, Console
        /// </summary>
        private readonly IInputOutputDevice inputOutputDevice;

        /// <summary>
        /// Provides descriptions(phrases) from a file
        /// </summary>
        private readonly IPhraseProvider phraseProvider;

        /// <summary>
        /// Defines board
        /// </summary>
        private readonly IBoard board;

        /// <summary>
        /// Defines class with figures
        /// </summary>
        private readonly IFigureProvider figureProvider;

        /// <summary>
        /// Defines game settings
        /// </summary>
        private readonly GameSettings gameSettings;

        /// <summary>
        /// simpleDot Flag - is it on the board
        /// </summary>
        private bool simpleDotFlag;

        /// <summary>
        /// simpleDot Flag - is it on the board
        /// </summary>
        private bool horizontalLineFlag;

        /// <summary>
        /// simpleDot Flag - is it on the board
        /// </summary>
        private bool verticalLineFlag;

        /// <summary>
        /// simpleDot Flag - is it on the board
        /// </summary>
        private bool curveFlag;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="settingsProvider">The settingsProvider<see cref="ISettingsProvider"/></param>
        /// <param name="inputOutputDevice">The inputOutputDevice<see cref="IInputOutputDevice"/></param>
        /// <param name="phraseProvider">The phraseProvider<see cref="IPhraseProvider"/></param>
        /// <param name="board">The board<see cref="IBoard"/></param>
        /// <param name="figureProvider">The figureProvider<see cref="IFigureProvider"/></param>
        public Game(
            ISettingsProvider settingsProvider, 
            IInputOutputDevice inputOutputDevice, 
            IPhraseProvider phraseProvider, 
            IBoard board, 
            IFigureProvider figureProvider)
        {
            this.settingsProvider = settingsProvider;
            this.inputOutputDevice = inputOutputDevice;
            this.phraseProvider = phraseProvider;
            this.board = board;
            this.figureProvider = figureProvider;            

            this.gameSettings = this.settingsProvider.GetGameSettings();
        }

        /// <summary>
        /// Delegate that used for output figures
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        private delegate void Draw(IBoard board);

        /// <summary>
        /// Game logic start method
        /// </summary>
        public void Start()
        {
            this.phraseProvider.SetLanguage(this.gameSettings.Language);

            this.board.BoardSizeX = this.gameSettings.Length;
            this.board.BoardSizeY = this.gameSettings.Width;            
            
            Draw draw = delegate(IBoard board) { }; // Create anonymus delegate for initialize delegate Draw

            this.board.Create();
            string enteredString = string.Empty;

            while (enteredString != null && !enteredString.Equals(this.gameSettings.ExitCode))
            {
                this.inputOutputDevice.SetPosition(0, this.gameSettings.Width);
                this.inputOutputDevice.WriteLineOutput(this.phraseProvider.GetPhrase("Description"));
                this.inputOutputDevice.WriteOutput(this.phraseProvider.GetPhraseAndReplace("Enter", "@ExitCode", this.gameSettings.ExitCode));

                enteredString = this.inputOutputDevice.ReadInput();
                
                switch (enteredString)
                {
                    case "1":                        
                        if (this.simpleDotFlag)
                        {
                            draw -= this.figureProvider.SimpleDot;
                        }
                        else
                        {
                            draw += this.figureProvider.SimpleDot;
                        }

                        this.simpleDotFlag = !this.simpleDotFlag;
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

                this.inputOutputDevice.Clear();
                this.board.Create();
                draw(this.board);
            }
        }
    }
}
