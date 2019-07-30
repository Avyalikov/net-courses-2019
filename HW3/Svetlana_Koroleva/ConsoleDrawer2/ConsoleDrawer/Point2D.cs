// <copyright file="Point2D.cs" company="SKorol">
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
    /// Point2D description
    /// </summary>
    public class Point2D
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        public Point2D(int x, int y)
        {
            this.XCoordinate = x;
            this.YCoordinate = y;
        }

        public enum MoveDirection{ 
            Right,
            Left,
            Up,
            Down,
            RightDown,
            LeftUp,
            RightUp,
            LeftDown,
            None

        }

       public void Move(MoveDirection direction,int interval)
        {
            switch(direction)
            {
                case MoveDirection.Right:
                    this.XCoordinate += interval;
                    break;
                case MoveDirection.Left:
                    this.XCoordinate -= interval;
                    break;
                case MoveDirection.Up:
                    this.YCoordinate -= interval;
                    break;
                case MoveDirection.Down:
                    this.YCoordinate += interval;
                    break;
                case MoveDirection.RightDown:
                    this.XCoordinate += interval;
                    this.YCoordinate += interval;
                    break;
                case MoveDirection.LeftUp:
                    this.XCoordinate -= interval;
                    this.YCoordinate -= interval;
                    break;
                case MoveDirection.RightUp:
                    this.XCoordinate += interval;
                    this.YCoordinate -= interval;
                    break;
                case MoveDirection.LeftDown:
                    this.XCoordinate -= interval;
                    this.YCoordinate += interval;
                    break;
                case MoveDirection.None:
                    break;

            }
            
        }
    }
}
