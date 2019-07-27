namespace GameWhichCanDraw
{
    using System;
    using System.Collections.Generic;
    using GameWhichCanDraw.Interfaces;

    internal class Game
    {
        private readonly ISettingsProvider settingsProvider;
        private readonly IInputOutputDevice inputOutputDevice;
        private readonly IPhraseProvider phraseProvider;
        private readonly IBoard board;
        private readonly IFigureProvider figureProvider;

        private readonly GameSettings gameSettings;

        private bool simpleDotFlag;
        private bool horizontalLineFlag;
        private bool verticalLineFlag;
        private bool curveFlag;

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

        private delegate void Draw(IBoard board);

        public void Start()
        {
            this.phraseProvider.SetLanguage(this.gameSettings.Language);

            this.board.BoardSizeX = this.gameSettings.Length;
            this.board.BoardSizeY = this.gameSettings.Width;            
            
            Draw draw = delegate (IBoard board) { }; // Create anonymus delegate for initialize delegate Draw

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
