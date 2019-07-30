using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame
{
    class Game
    {

        private readonly ISettingsProvider settingsProvider;
        private readonly IInputOutputDevice ioDevice;
        private readonly IPhraseProvider phraseProvider;
        private readonly IBoard board;
        private readonly IFigureDrawing figureDrawing;

        private readonly GameSettings gameSettings;



        public Game(ISettingsProvider settingsProvider, IInputOutputDevice ioDevice, IPhraseProvider phraseProvider, IBoard board, IFigureDrawing figureDrawing)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
            this.phraseProvider = phraseProvider;
            this.ioDevice = ioDevice;
            this.board = board;
            this.figureDrawing = figureDrawing;
        }

        private delegate void Draw(IBoard board);

        public void Run()
        {
            string userNumber="";
            int FigureCount = 1;
            int[] NumArray = { 1, 2, 3, 4 };
            List<int> userArray = new List<int> { 1 };
            IBoard ConsoleBoard = new Board();
            ConsoleBoard.boardSizeX = gameSettings.HorizontalBoardSize;
            ConsoleBoard.boardSizeY = gameSettings.VerticalBoardSize;

            

            ioDevice.WriteOutput(phraseProvider.GetPhrase("Welcome"));
            ioDevice.WriteOutput(phraseProvider.GetPhrase("SelectFigure"));
            int OrigX = ConsoleBoard.OrigX;
            int OrigY = ConsoleBoard.OrigY;


            ConsoleBoard.Draw(ConsoleBoard);

            Draw drawDot = figureDrawing.DrawDot;            
            Draw drawVerticalLine = figureDrawing.DrawVerticalLine;
            Draw drawHorizontalLine = figureDrawing.DrawHorisontalLine;
            Draw drawSquare = figureDrawing.DrawSquare;

            while (userNumber != gameSettings.ExitCode.ToLower())
            {
                ioDevice.SetCursorPosition(OrigX,OrigY);
                userNumber = ioDevice.ReadOutput();
                Boolean isSuccsess = int.TryParse(userNumber, out int temp);
                if (isSuccsess && (Array.IndexOf(NumArray, temp)!=-1))
                {
                    userArray.Add(temp);

                    
                    ConsoleBoard.boardSizeX /= FigureCount;
                    ConsoleBoard.boardSizeY /= FigureCount;
                    switch (userNumber)
                    {
                        case "1":
                            drawDot(ConsoleBoard);
                            break;
                        case "2":
                             drawVerticalLine(ConsoleBoard);
                             break;
                         case "3":
                             drawHorizontalLine(ConsoleBoard);
                             break;
                         case "4":
                            ConsoleBoard.boardSizeX /= FigureCount;
                            ConsoleBoard.boardSizeY /= FigureCount;
                            drawSquare(ConsoleBoard);
                            break;
                    }
                    FigureCount++;
                }
                else
                {
                    ioDevice.SetCursorPosition(OrigX, OrigY-1);
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("PutNumber"));
                }

            }
        }
    }
}