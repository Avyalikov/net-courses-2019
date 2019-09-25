// <copyright file="DrawSettings.cs" company="SKorol">
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
    /// DrawSettings description
    /// </summary>
    public class DrawSettings
    {
        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }
        public int StartCoordinateX { get; set; }
        public int StartCoordinateY { get; set; }
        public string HorizontalFiller { get; set; }
        public string VerticalFiller { get; set; }
        public string CornerFiller { get; set; }
        public string SlashFiller { get; set; }
        public string Language { get; set; }
        public string Exit { get; set; }
        public int InfoCoordinateY { get; set; }
        public int InputCoordinateY { get; set; }
        public int OutputCoordinateY { get; set; }
        public int WrongNumCoordinateY { get; set; }
        public int ExitCoordinateY { get; set; }
        private Point2D firstFigPos;
        public Point2D FirstFigPos
        {
            get
            {
                return firstFigPos;
            }
          set
            {
                this.firstFigPos = new Point2D(this.StartCoordinateX + 1, this.StartCoordinateY + 1);
            }
        }
        private Point2D secondFigPos;
        public Point2D SecondFigPos
        {
            get
            {
                return secondFigPos;
            }
          set
            {
                this.secondFigPos = new Point2D(this.StartCoordinateX + 1+this.BoardSizeX/4, this.StartCoordinateY + 1);
            }
        }

        private Point2D thirdFigPos;
        public Point2D ThirdFigPos
        {
            get
            {
                return thirdFigPos;
            }
       set
            {
                this.thirdFigPos = new Point2D(this.StartCoordinateX + 1 + this.BoardSizeX / 2, this.StartCoordinateY + 1);
            }
        }

        private Point2D fhFigPos;
        public Point2D FhFigPos
        {
            get
            {
                return fhFigPos;
            }
          set
            {
                this.fhFigPos = new Point2D(this.StartCoordinateX + 1 + this.BoardSizeX*3/4
                    , this.StartCoordinateY + 1);
            }
        }
        private Dictionary<int, Point2D> startPoints;
        public Dictionary<int, Point2D> Startpoints{
            get { return startPoints; }
            set { startPoints = new Dictionary<int, Point2D>();
                startPoints.Add(1, FirstFigPos);
                startPoints.Add(2, SecondFigPos);
                startPoints.Add(3, ThirdFigPos);
                startPoints.Add(4, fhFigPos);

            } }
    }
}
