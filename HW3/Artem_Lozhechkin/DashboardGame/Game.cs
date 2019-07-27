using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardGame
{
    class Game
    {
        private ConsoleBoard board;
        private GameMenu menu;

        public Game(ConsoleBoard board, GameMenu menu)
        {
            this.board = board;
            this.menu = menu;
            do
            {
                board.Clear();
                this.menu.ShowMenu();
                while (!this.menu.ShowUserChoice(board.ReadLine())) { }
                board.Clear();
                board.DrawBoard();
                menu.DrawFigures(board);
                board.DrawLines();
                menu.DrawFigures = null;
            } while (Console.ReadLine() != "E");
        }
    }
}
