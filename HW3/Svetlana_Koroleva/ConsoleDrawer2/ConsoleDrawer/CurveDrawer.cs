// <copyright file="CurveDrawer.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace ConsoleDrawer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// CurveDrawer description
    /// </summary>
    public class CurveDrawer : ICurveDrawer
                
    {
     
        private readonly IIOComponent iOComponent = new ConsoleIOComponent();
        DrawSettings drawSettings;
        public CurveDrawer(DrawSettings drawSettings)
        {
            this.drawSettings = drawSettings;
            
        }


        public void DrawAnotherCurve(IBoard board)
        {
           
            if(board.GetSizeSideX()>board.GetSizeSideY())
            {
                board.GetCurrenttPosition().YCoordinate = (board.GetSizeSideY() / 4) - 2 + board.GetStartPosition().YCoordinate;
                board.GetCurrenttPosition().XCoordinate++;
                DrawLine((board.GetSizeSideY() / 4) - 2, drawSettings.SlashFiller, 1, Point2D.MoveDirection.RightUp, board.GetCurrenttPosition());
            }
            else
            {
                board.GetCurrenttPosition().YCoordinate = (board.GetSizeSideY() / 4) - 2 + board.GetStartPosition().YCoordinate;
                board.GetCurrenttPosition().XCoordinate++;
                DrawLine((board.GetSizeSideX() / 4) - 2, drawSettings.SlashFiller, 1, Point2D.MoveDirection.RightUp, board.GetCurrenttPosition());
            }
            
        }


        public void DrawDot(IBoard board)
        {
            Point2D startDrawPoint = board.GetCurrenttPosition();
            startDrawPoint.XCoordinate++;
            startDrawPoint.YCoordinate++;
            DrawLine(1, ".", 1, Point2D.MoveDirection.Right, board.GetCurrenttPosition());
        }

        public void DrawHorizontalLine(IBoard board)
        {
           DrawLine((board.GetSizeSideX() / 4)-1, drawSettings.HorizontalFiller, 1, Point2D.MoveDirection.Right, board.GetCurrenttPosition());
        }

        public void DrawVerticalLine(IBoard board)
        {
            Point2D startDrawPoint = board.GetCurrenttPosition();
            startDrawPoint.XCoordinate++;
            startDrawPoint.YCoordinate=board.GetStartPosition().YCoordinate+1;
            DrawLine((board.GetSizeSideY()/4)-1, drawSettings.VerticalFiller, 1, Point2D.MoveDirection.Down, startDrawPoint);
        }



        public void DrawSymbolWithOffset(string symbol, int step, Point2D.MoveDirection direction, Point2D currentPoint)
        {
            try
            {

                iOComponent.SetCursor(currentPoint.XCoordinate, currentPoint.YCoordinate);
                iOComponent.WriteOutput(symbol);
                currentPoint.Move(direction, step);
            }
            catch (ArgumentOutOfRangeException e)
            {
                iOComponent.Clear();
                iOComponent.WriteOutput(e.Message);
            }

        }

        public void DrawLine(int amountOfSymbols, string symbol, int step, Point2D.MoveDirection direction, Point2D point)
        {
            for (int i = 0; i < amountOfSymbols; i++)
            {
                DrawSymbolWithOffset(symbol, step, direction, point);
            }
        }



        public void DrawAt(string symbol, int xCoordinate, int yCoordinate)
        {
            iOComponent.SetCursor(xCoordinate, yCoordinate);
            iOComponent.WriteOutput(symbol);


        }
    }
}
