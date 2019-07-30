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

            Stack<int> i = board.GetInputes();
            
            Point2D position = new Point2D(drawSettings.Startpoints[i.Peek()].XCoordinate, drawSettings.Startpoints[i.Peek()].YCoordinate+drawSettings.BoardSizeY/2);
           
          DrawLine((board.GetSizeSideY() / 4) , drawSettings.SlashFiller, 1, Point2D.MoveDirection.RightUp, position);
           
            
        }


        public void DrawDot(IBoard board)
        {

            Stack<int> i = board.GetInputes();
         
            Point2D position = new Point2D(drawSettings.Startpoints[i.Peek()].XCoordinate, drawSettings.Startpoints[i.Peek()].YCoordinate);
           
            DrawLine(1, ".", 1, Point2D.MoveDirection.Right, position);

        }

        public void DrawHorizontalLine(IBoard board)
        {
            Stack<int> i = board.GetInputes();
            
            Point2D position = new Point2D(drawSettings.Startpoints[i.Peek()].XCoordinate, drawSettings.Startpoints[i.Peek()].YCoordinate);

            DrawLine((board.GetSizeSideX() / 4)-2, drawSettings.HorizontalFiller, 1, Point2D.MoveDirection.Right, position);//board.GetCurrenttPosition()
        }

        public void DrawVerticalLine(IBoard board)
        {
            Stack<int> i = board.GetInputes();
            
            Point2D position = new Point2D(drawSettings.Startpoints[i.Peek()].XCoordinate, drawSettings.Startpoints[i.Peek()].YCoordinate);
         
            DrawLine(board.GetSizeSideY()-2, drawSettings.VerticalFiller, 1, Point2D.MoveDirection.Down, position);
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
