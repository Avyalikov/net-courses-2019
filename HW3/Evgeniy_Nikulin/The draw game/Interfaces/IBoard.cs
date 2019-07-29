//-----------------------------------------------------------------------
// <copyright file="IBoard.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Interfaces
{
    /// <summary>
    /// IBoard interface
    /// </summary>
    public interface IBoard
    {
        /// <summary>
        /// Gets or sets Width
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets Height
        /// </summary>
        int Height { get; set; }

        /// <summary>
        ///  Draw board
        /// </summary>
        /// <param name="board">Board component</param>
        void Draw(IBoard board);
    }
}