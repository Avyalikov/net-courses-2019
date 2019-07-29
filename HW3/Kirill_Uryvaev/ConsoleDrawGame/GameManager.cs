using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDrawGame
{
    class GameManager
    {
        GameSettings settings;

        delegate void Draw(IBoard board);

        private readonly IBoard board;
        private readonly IInputOutputProvider inputOutputProvider;
        private readonly ISettingsProvider settingsProvider;
        private readonly IFigureProvider figureProvider;

        public GameManager(IInputOutputProvider inputOutputProvider, ISettingsProvider settingsProvider, IFigureProvider figureProvider, IBoard board)
        {
            this.inputOutputProvider = inputOutputProvider;
            this.settingsProvider = settingsProvider;
            this.figureProvider = figureProvider;
            this.board = board;

            settings = settingsProvider.GetSettings();
            board.SetBoardSize(settings.BoardWidth, settings.BoardHeight);
        }

        public void Run()
        {
            string key = "";
            string exitCode = settings.ExitString.ToLower();
            Draw drawOnDashboard = board.DrawBoard;
            while (key.ToLower()!= exitCode)
            {
                board.Clear();
                drawOnDashboard.Invoke(board);
                key = inputOutputProvider.Read();
                switch (key)
                {
                    case "1":
                        drawOnDashboard += figureProvider.DrawDot;
                        break;
                    case "2":
                        drawOnDashboard += figureProvider.DrawHorizontalLine;
                        break;
                    case "3":
                        drawOnDashboard += figureProvider.DrawVerticalLine;
                        break;
                    case "4":
                        drawOnDashboard += figureProvider.DrawSinus;
                        break;
                    case "5":
                        drawOnDashboard = board.DrawBoard;
                        break;
                }
                
            }
            
        }
    }
}
