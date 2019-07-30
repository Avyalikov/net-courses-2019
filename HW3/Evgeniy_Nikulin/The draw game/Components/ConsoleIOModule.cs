//-----------------------------------------------------------------------
// <copyright file="ConsoleIOModule.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Components
{
    using System;
    using System.Collections;
    using Components.DataTypes;
    using Interfaces;

    /// <summary>
    /// Input/Output component
    /// </summary>
    public class ConsoleIOModule : IInputOutput
    {
        /// <summary>
        /// Set window size
        /// </summary>
        /// <param name="width">Window width</param>
        /// <param name="height">Window height</param>
        public void SetWindowSize(int width, int height)
        {
            try
            {
                Console.SetWindowSize(width, height);
                Console.SetBufferSize(width, height);
            }
            catch (Exception)
            {
                throw new Exception("Window size is very big.");
            }
        }

        /// <summary>
        /// Set cursor position on window
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public void SetCursorPosition(int x, int y) => Console.SetCursorPosition(x, y);

        /// <summary>
        /// Clear the window
        /// </summary>
        public void Clear() => Console.Clear();

        /// <summary>
        /// Method for input a value
        /// </summary>
        /// <returns>user's input</returns>
        public string Input() => Console.ReadLine();

        /// <summary>
        /// Method for output a value
        /// </summary>
        /// <param name="str">Printed string</param>
        /// <param name="end">String ending</param>
        public void Print(string str = null, string end = null) => 
            Console.Write($"{str ?? string.Empty}{end ?? Environment.NewLine}");

        /// <summary>
        /// Method for output a array
        /// </summary>
        /// <param name="arr">Printed array</param>
        /// <param name="sep">String between array's item</param>
        /// <param name="end">String ending</param>
        public void Print(IEnumerable arr, string sep = " ", string end = null)
        {
            bool isFirst = true;
            foreach (var item in arr)
            {
                this.Print($"{(isFirst ? string.Empty : sep)}{item}", end: string.Empty);
                isFirst = false;
            }

            this.Print(end: end);
        }

        /// <summary>
        /// Print the point
        /// </summary>
        /// <param name="point">This is point</param>
        /// <param name="stile">Stile of point</param>
        public void Print(Point point, string stile)
        {
            this.SetCursorPosition(point.X, point.Y);
            this.Print(stile, end: string.Empty);
        }
    }
}