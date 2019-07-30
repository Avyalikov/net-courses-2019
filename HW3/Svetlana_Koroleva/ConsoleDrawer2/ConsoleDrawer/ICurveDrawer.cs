// <copyright file="ICurveDrawer.cs" company="SKorol">
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
    /// ICurveDrawer description
    /// </summary>
    public interface ICurveDrawer
    {

        void DrawDot(IBoard board);
        void DrawHorizontalLine(IBoard board);
        void DrawVerticalLine(IBoard board);
        void DrawAnotherCurve(IBoard board);

        void DrawAt(string symbol, int xCoordinate, int yCoordinate);
        void DrawSymbolWithOffset(string symbol, int step, Point2D.MoveDirection direction, Point2D startPoint);
        void DrawLine(int amountOfSymbols, string symbol, int step, Point2D.MoveDirection direction, Point2D startPoint);
    }
}
