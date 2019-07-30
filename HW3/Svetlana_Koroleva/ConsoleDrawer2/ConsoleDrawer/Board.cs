// <copyright file="Board.cs" company="SKorol">
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
    /// Board description
    /// </summary>
    public class Board : IBoard
    {
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        private readonly Point2D startPoint;
        private Point2D currentPoint;
        delegate void Draw(string s, int interval);
        private readonly ICurveDrawer curveDrawer;
        public Stack<int> inputes = new Stack<int>();

        public Stack<int> GetInputes()
        {
            return this.inputes;
        }


        public Board(ICurveDrawer curveDrawer)
        {
            this.curveDrawer = curveDrawer;
            this.inputes = new Stack<int>();
        }

        public Board(ICurveDrawer curveDrawer,
            int x, int y, int xStart, int yStart)
        {
            this.curveDrawer = curveDrawer;
            this.startPoint = new Point2D(xStart, yStart);
            this.currentPoint = new Point2D(xStart, yStart);
            this.SizeX = x;
            this.SizeY = y;
        }


        private bool Condition(Point2D.MoveDirection moveDirection)
        {

            switch (moveDirection)
            {
                case Point2D.MoveDirection.Right:
                    {
                        return currentPoint.XCoordinate != (this.startPoint.XCoordinate + this.SizeX - 1);

                    }

                case Point2D.MoveDirection.Down:
                    {
                        return currentPoint.YCoordinate != (this.startPoint.YCoordinate + this.SizeY - 1);
                    }
                case Point2D.MoveDirection.Left:
                    {
                        return currentPoint.XCoordinate != this.startPoint.XCoordinate;
                    }
                case Point2D.MoveDirection.Up:
                    {
                        return currentPoint.YCoordinate != this.startPoint.YCoordinate;
                    }
            }
            return false;
        }



        public void DrawBoardFragment(string corner, string symb, Point2D.MoveDirection direction, int lenght)
        {
            this.curveDrawer.DrawAt(corner, currentPoint.XCoordinate, currentPoint.YCoordinate);
            currentPoint.Move(direction, 1);
            bool condition = Condition(direction);
            while (condition)
            {
                this.curveDrawer.DrawLine(lenght - 1, symb, 1, direction, currentPoint);
                condition = Condition(direction);

            }
        }

        public void DrawBoard(string gorizontal, string vertical, string corner)
        {
            DrawBoardFragment(corner, gorizontal, Point2D.MoveDirection.Right, SizeX - 1);
            DrawBoardFragment(corner, vertical, Point2D.MoveDirection.Down, SizeY - 1);
            DrawBoardFragment(corner, gorizontal, Point2D.MoveDirection.Left, SizeX - 1);
            DrawBoardFragment(corner, vertical, Point2D.MoveDirection.Up, SizeY - 1);
            currentPoint.Move(Point2D.MoveDirection.RightDown, 2);
        }




        public Point2D GetStartPosition()
        {
            return this.startPoint;
        }

        public Point2D GetCurrenttPosition()
        {
            return this.currentPoint;
        }

        public int GetSizeSideX()
        {
            return this.SizeX;
        }

        public int GetSizeSideY()
        {
            return this.SizeY;
        }

        public void SetPosition(Point2D point)
        {
            this.currentPoint.XCoordinate = point.XCoordinate;
            this.currentPoint.YCoordinate = point.YCoordinate;
        }
    }
}
