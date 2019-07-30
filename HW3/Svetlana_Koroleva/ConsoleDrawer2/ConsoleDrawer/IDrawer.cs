// <copyright file="IDrawer.cs" company="SKorol">
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
    /// IDrawer description
    /// </summary>
    public interface IDrawer
    {
        Dictionary<int, Point2D> GetInputesPos();
    }
}
