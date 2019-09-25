// <copyright file="IIOComponent.cs" company="SKorol">
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
    /// IIOComponent description
    /// </summary>
    public interface IIOComponent
    {
        string ReadInput();

        void WriteOutput(string data);

        void Clear();

        void SetCursor(int x, int y);

        void ClearRow(int y);
    }
}
