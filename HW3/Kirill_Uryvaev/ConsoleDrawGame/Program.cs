using System;

namespace ConsoleDrawGame
{
    class Program
    {
        static void Main(string[] args)
        {
            IBoard board = new ConsoleBoard();
            IInputOutputProvider inputOutputProvider = new ConsoleIOProvider();
            ISettingsProvider settingsProvider = new FileSettingsProvider();
            IFigureProvider figureProvider = new HardcodeFigureProvider();
            GameManager game = new GameManager(inputOutputProvider, settingsProvider, figureProvider,board);
            game.Run();
        }
    }
}
