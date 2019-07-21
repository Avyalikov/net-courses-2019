using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDrawGame
{
    delegate void Draw(IBoard board);
    class GameManager
    {
        GameSettings settings;
        Dictionary<string, Draw> figures;

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
            figures = figureProvider.GetFigures();
            board.SetBoardSize(settings.BoardWidth, settings.BoardHeight);
        }

        public void Run()
        {
            string key = "";
            string exitCode = settings.ExitString.ToLower();
            board.DrawBoard();
            while (key.ToLower()!= exitCode)
            {
                key = inputOutputProvider.Read();
                if (figures.ContainsKey(key))
                {
                    figures[key].Invoke(board);
                }
                else
                {
                    board.DrawAt('\n', 0, 0);
                }
            }
            
        }
    }
}
