//-----------------------------------------------------------------------
// <copyright file="IFigureManager.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Interfaces
{
    /// <summary>
    /// IFigureManager interface
    /// </summary>
    public interface IFigureManager
    {
        /// <summary>
        ///  Draw first figure
        /// </summary>
        /// <param name="board">Board component</param>
        void DrawFirst(IBoard board);

        /// <summary>
        ///  Draw second figure
        /// </summary>
        /// <param name="board">Board component</param>
        void DrawSecond(IBoard board);

        /// <summary>
        ///  Draw third figure
        /// </summary>
        /// <param name="board">Board component</param>
        void DrawThird(IBoard board);

        /// <summary>
        ///  Draw fourth figure
        /// </summary>
        /// <param name="board">Board component</param>
        void DrawFourth(IBoard board);
    }
}
