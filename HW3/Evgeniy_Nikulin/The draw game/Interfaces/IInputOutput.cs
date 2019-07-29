//-----------------------------------------------------------------------
// <copyright file="IInputOutput.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Interfaces
{
    using System.Collections;
    using Components.DataTypes;

    /// <summary>
    /// Input/Output interface
    /// </summary>
    public interface IInputOutput
    {
        /// <summary>
        /// Set window size
        /// </summary>
        /// <param name="width">Window width</param>
        /// <param name="height">Window height</param>
        void SetWindowSize(int width, int height);

        /// <summary>
        /// Clear the window
        /// </summary>
        void Clear();

        /// <summary>
        /// Method for input a value
        /// </summary>
        /// <returns>user's input</returns>
        string Input();

        /// <summary>
        /// Set cursor position on window
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        void SetCursorPosition(int x, int y);

        /// <summary>
        /// Method for output a value
        /// </summary>
        /// <param name="str">Printed string</param>
        /// <param name="end">String ending</param>
        void Print(string str = null, string end = null);

        /// <summary>
        /// Method for output a array
        /// </summary>
        /// <param name="arr">Printed array</param>
        /// <param name="sep">String between array's item</param>
        /// <param name="end">String ending</param>
        void Print(IEnumerable arr, string sep = " ", string end = null);

        /// <summary>
        /// Print the point
        /// </summary>
        /// <param name="point">This is point</param>
        /// /// <param name="stile">This is point stile</param>
        void Print(Point point, string stile);
    }
}