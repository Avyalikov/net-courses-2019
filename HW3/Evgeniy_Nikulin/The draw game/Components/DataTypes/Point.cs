//-----------------------------------------------------------------------
// <copyright file="Point.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Components.DataTypes
{
    /// <summary>
    /// Point structure
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Point" /> struct.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets x coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets y coordinate
        /// </summary>
        public int Y { get; set; }
    }
}