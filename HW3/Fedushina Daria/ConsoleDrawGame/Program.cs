using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame
{
    class Program
    {
        static void Main(string[] args)
        {
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new PhraseProvider(settingsProvider);
            IInputOutputDevice ioDevice = new ConsoleIO();
            IBoard board = new Board(ioDevice);
            IFigureDrawing figureDrawing = new FigureDrawing();


            var game = new Game(settingsProvider, ioDevice, phraseProvider, board, figureDrawing);
            game.Run();
        }
    }
}
