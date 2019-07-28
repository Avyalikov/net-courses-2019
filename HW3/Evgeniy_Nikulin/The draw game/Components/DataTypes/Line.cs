//-----------------------------------------------------------------------
// <copyright file="Line.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Components.DataTypes
{
    using System.Collections.Generic;

    /// <summary>
    /// Line class
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line" /> class.
        /// </summary>
        /// <param name="start">Start point</param>
        /// <param name="finish">Finish point</param>
        public Line(Point start, Point finish)
        {
            this.Body = new List<Point>();

            if (start.X == finish.X)
            {
                for (int i = start.Y; i <= finish.Y; i++)
                {
                    this.Body.Add(new Point(start.X, i));
                }
            }
            else if (start.Y == finish.Y)
            {
                for (int i = start.X; i <= finish.X; i += 2)
                {
                    this.Body.Add(new Point(i, start.Y));
                }
            }
        }

        /// <summary>
        /// Gets or sets line's points
        /// </summary>
        public List<Point> Body { get; set; }
    }
}