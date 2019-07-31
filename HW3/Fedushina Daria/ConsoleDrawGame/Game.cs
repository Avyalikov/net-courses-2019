using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame
{
    class Game
    {
        private readonly IInputOutputDevice ioDevice;
        private readonly IPhraseProvider phraseProvider;
        private readonly IFigureDrawing figureDrawing;

        private readonly GameSettings gameSettings;

        public Game(ISettingsProvider settingsProvider, IInputOutputDevice ioDevice, IPhraseProvider phraseProvider, IBoard board, IFigureDrawing figureDrawing)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
            this.phraseProvider = phraseProvider;
            this.ioDevice = ioDevice;
            this.figureDrawing = figureDrawing;
        }

        private delegate void Draw(IBoard board);

        public void Run()
        {
            string userNumber=String.Empty;
            int[] NumArray = { 1, 2, 3, 4 };
            int countFigures=0;
            IBoard consoleBoard = new Board(ioDevice);
            consoleBoard.boardSizeX = gameSettings.HorizontalBoardSize;
            consoleBoard.boardSizeY = gameSettings.VerticalBoardSize;
            Draw draw = new Draw(consoleBoard.DrawBoard);              //define an instance of delegate

            ioDevice.WriteOutput(phraseProvider.GetPhrase("Welcome"));       
            ioDevice.WriteOutput(phraseProvider.GetPhrase("SelectFigure"));
            int OX = consoleBoard.OX;                                          
            int OY = consoleBoard.OY;
               
            draw(consoleBoard);                                     //drawing board

            while (userNumber.ToLower() != gameSettings.ExitCode.ToLower())       //untill user put 'exit' word
            {
                
                ioDevice.SetCursorPosition(OX,OY);                      //put cursor above the board (the board is one point lower than a originall dot)
                ioDevice.ClearRow(OY);                                  // (c)method by  Svetlana Koroleva
                userNumber = ioDevice.ReadOutput();
                Boolean isSuccsess = int.TryParse(userNumber, out int temp);
                if ((countFigures != 4) && isSuccsess && (Array.IndexOf(NumArray, temp)!=-1))  //if there is less than 4 figures on the board
                {
                    countFigures++;                                   // count figures
                    switch (userNumber)
                        {
                            case "1":                               
                            draw = figureDrawing.DrawDot;             //define a meaning of an delegate at this moment
                                break;
                            case "2":
                            draw = figureDrawing.DrawVerticalLine;
                            break;
                            case "3":
                            draw = figureDrawing.DrawHorisontalLine;
                            break;
                            case "4":
                            draw = figureDrawing.DrawSquare;
                            break;
                        }
                    draw(consoleBoard);                                             //draw figure
                }
                else if (userNumber == "0" || countFigures == 4)                //if user put 0 the game starts from the clean board
                {
                    ioDevice.Clear();
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("Welcome"));
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("SelectFigure"));
                    draw = consoleBoard.DrawBoard;
                    draw(consoleBoard);
                    countFigures = 0;
                }
                else
                {
                    ioDevice.SetCursorPosition(OX, OY-2);
                    ioDevice.WriteWithStayOnLine(phraseProvider.GetPhrase("PutNumber"));
                }
                
            }
        }
    }
}