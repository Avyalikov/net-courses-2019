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
        public string CornerFiller { get; set;}
        public string SlashFiller { get; set; }
        public string Language { get; set; }
        public string Exit { get; set; }
        public int InfoCoordinateY { get; set; }
        public int InputCoordinateY { get; set; }
        public int WrongNumCoordinateY { get; set; }
        public int ExitCoordinateY { get; set; }
    }
}
