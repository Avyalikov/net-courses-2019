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
        }
        public void Play()
        {
            string userInput;
            do
            {
                board.Clear();
                menu.ShowInfo();
                do
                {
                    userInput = board.ReadLine();
                } while (!menu.ParseUserChoice(userInput));
                board.Clear();
                board.DrawBoard();
                menu.DrawFigures(board);
                board.DrawAxis();
                menu.DrawFigures = null;
            } while (Console.ReadKey().Key == ConsoleKey.E);
        }
    }
}
