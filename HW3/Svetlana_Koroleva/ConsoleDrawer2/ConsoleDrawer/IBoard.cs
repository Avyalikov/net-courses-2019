// <copyright file="IBoard.cs" company="SKorol">
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
    /// ClassMy1 description
    /// </summary>
    public interface IBoard
    {
        void DrawBoard(string gorizontal, string vertical, string corner);
        Point2D GetStartPosition();
        Point2D GetCurrenttPosition();
        void SetPosition(Point2D point);
        int GetSizeSideX();
        int GetSizeSideY();
        Stack<int> GetInputes();

    }
}
