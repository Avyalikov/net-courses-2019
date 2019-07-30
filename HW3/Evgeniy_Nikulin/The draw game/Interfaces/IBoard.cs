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
        /// Gets or sets boardSizeX
        /// </summary>
        int boardSizeX { get; set; }

        /// <summary>
        /// Gets or sets boardSizeY
        /// </summary>
        int boardSizeY { get; set; }

        /// <summary>
        ///  Draw board
        /// </summary>
        /// <param name="board">Board component</param>
        void Draw(IBoard board);
    }
}