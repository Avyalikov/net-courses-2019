namespace GameWhichCanDraw
{
    using System;
    using GameWhichCanDraw.Interfaces;

    internal class Game
    {
        delegate void Draw(IBoard board);
        private bool exitFlag;
        private bool simpleDotFlag;
        private bool horizontalLineFlag;
        private bool verticalLineFlag;
        private bool curveFlag;

        public void Start()
        {
            int length = 25;
            int width = 30;

            IBoard dashBoard = new Components.DashBoard(length, width);            
            IFigureProvider figureProvider = new Components.FigureProvider();
            
            Draw draw = figureProvider.Empty;

            dashBoard.Create();

            while (!exitFlag)
            {
                Console.SetCursorPosition(0, width);
                Console.Write("Enter the number: ");

                string enteredString = Console.ReadLine();
                
                switch (enteredString)
                {
                    case "1":                        
                        if (simpleDotFlag)
                        {
                            draw -= figureProvider.SimpleDot;
                        }
                        else
                        {
                            draw += figureProvider.SimpleDot;
                        }
                        simpleDotFlag = !simpleDotFlag;
                        break;
                    case "2":                        
                        if (horizontalLineFlag)
                        {
                            draw -= figureProvider.HorizontalLine;
                        }
                        else
                        {
                            draw += figureProvider.HorizontalLine;
                        }
                        horizontalLineFlag = !horizontalLineFlag;
                        break;
                    case "3":
                        if (verticalLineFlag)
                        {
                            draw -= figureProvider.VerticalLine;
                        }
                        else
                        {
                            draw += figureProvider.VerticalLine;
                        }
                        verticalLineFlag = !verticalLineFlag;
                        break;
                    case "4":
                        if (curveFlag)
                        {
                            draw -= figureProvider.Curve;
                        }
                        else
                        {
                            draw += figureProvider.Curve;
                        }
                        curveFlag = !curveFlag;
                        break;
                    case "exit":
                        exitFlag = true;
                        break;
                    default:
                        break;
                }

                Console.Clear();
                dashBoard.Create();
                draw(dashBoard);
            }

            //// Console.ReadLine(); // pause
        }
    }
}
