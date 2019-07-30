// <copyright file="ConsoleIOComponent.cs" company="SKorol">
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
    /// ConsoleIOComponent description
    /// </summary>
    public class ConsoleIOComponent:IIOComponent
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void ClearRow(int y)
        {
            StringBuilder s = new StringBuilder();
            s.Length = 50;
            Console.ForegroundColor = ConsoleColor.Black;
            this.SetCursor(0, y);
            this.WriteOutput(s.ToString());
            this.SetCursor(0, y);
            Console.ForegroundColor = ConsoleColor.White;

        }

        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void SetCursor(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public void WriteOutput(string data)
        {
            Console.WriteLine(data);
        }
    }
}
