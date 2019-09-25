//-----------------------------------------------------------------------
// <copyright file="Rectangle.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Components.DataTypes
{
    using System.Collections.Generic;

    /// <summary>
    /// Rectangle class
    /// </summary>
    public class Rectangle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle" /> class.
        /// </summary>
        /// <param name="topLeft">Top - Left point</param>
        /// <param name="bottomRight">Bottom - Right point</param>
        public Rectangle(Point topLeft, Point bottomRight)
        {
            this.Body = new List<Point>();

            Point topRight = new Point(bottomRight.X, topLeft.Y);
            Point bottomLeft = new Point(topLeft.X, bottomRight.Y);

            Line top = new Line(topLeft, topRight);
            Line bottom = new Line(bottomLeft, bottomRight);
            Line right = new Line(topRight, bottomRight);
            Line left = new Line(topLeft, bottomLeft);
            
            foreach (var point in top.Body)
            {
                this.Body.Add(point);
            }

            foreach (var point in bottom.Body)
            {
                this.Body.Add(point);
            }

            foreach (var point in right.Body)
            {
                this.Body.Add(point);
            }

            foreach (var point in left.Body)
            {
                this.Body.Add(point);
            }
        }

        /// <summary>
        /// Gets or sets rectangle's points
        /// </summary>
        public List<Point> Body { get; set; }
    }
}