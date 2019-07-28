using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardGame
{
    class Game
    {
        private readonly ConsoleBoard board;
        private readonly GameMenu menu;

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
                board.DrawAxis();
                menu.DrawFigures(board);
                menu.DrawFigures = null;

            } while (board.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
