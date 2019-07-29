using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWhichCanDraw
{
    public class Game
    {
        private readonly IBoard board;
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutputDevice ioDevice;
        private readonly IDrawOnBoard drawOnBoard;
        private readonly ISettingsProvider settingsProvider;

        private readonly GameSettings gameSettings;

        public Game(
            IBoard board,
            IPhraseProvider phraseProvider,
            IInputOutputDevice ioDevice,
            IDrawOnBoard drawOnBoard,
            ISettingsProvider settingsProvider)
        {
            this.board = board;
            this.phraseProvider = phraseProvider;
            this.ioDevice = ioDevice;
            this.drawOnBoard = drawOnBoard;
            this.settingsProvider = settingsProvider;

            gameSettings = settingsProvider.GetGameSettings();
        }

        private delegate void Draw(IBoard board);

        private int UserInput()
        {
            int result = -1;
            int enteredNumber;
            do
            {
                var input = ioDevice.ReadInput();
                if (int.TryParse(input, out enteredNumber))
                {
                    result = 1;
                }
                else if (input.ToLowerInvariant().Equals(gameSettings.ExitCode))
                {
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("ThankYouForPlaying"));
                    result = 0;
                }
                else
                {
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("ItIsNotANumber"));
                }

            } while (result < 0);

            if (result == 0)
            {
                enteredNumber = -1;
            }
            return enteredNumber;
        }

        public void Run()
        {
            ioDevice.WriteOutput(phraseProvider.GetPhrase("Welcome"));
            board.boardSizeX = gameSettings.Length;
            board.boardSizeY = gameSettings.Width;
            int figure;
            Draw draw = new Draw(drawOnBoard.CleanBoard);
            draw += board.DrawBoard;


            while (true)
            {
                ioDevice.WriteOutput(phraseProvider.GetPhrase("TheFiguresAre"));
                ioDevice.WriteOutput(phraseProvider.GetPhrase("ChooseYourFigure"));

                figure = UserInput();
                if(figure < 0)
                { break; }

                if(figure == 1)
                {
                    draw += drawOnBoard.DrawSimpleDot;
                }
                if (figure == 2)
                {
                    draw += drawOnBoard.DrawHorizontalLine;
                }
                if (figure == 3)
                {
                    draw += drawOnBoard.DrawVerticalLine;
                }
                if (figure == 4)
                {
                    draw += drawOnBoard.DrawCircle;
                }
  
                draw(board);
                Console.SetCursorPosition(0, board.boardSizeY + 1);
            }
        }
    }
}
