    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

namespace GameWhichCanDraw
{    
    public class Program
    {
        static void Main(string[] args)
        {
            ISettingsProvider settingsProvider = new JsonSettingsProvider();
            GameSettings gameSettings = settingsProvider.GetGameSettings();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(gameSettings.Language);
            IInputOutputDevice inputOutputDevice = new ConsoleInputOutputDevice();
            IBoard board = new ConsoleBoard();
            IDrawOnBoard drawOnBoard = new DrawOnConsoleBoard();

            Game DrawingGame = new Game(board, phraseProvider, inputOutputDevice, drawOnBoard, settingsProvider);
            DrawingGame.Run();

            Console.ReadKey();

        }
    }
}
