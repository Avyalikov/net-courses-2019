// <copyright file="ConsoleInputOutputDevice.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw.Components
{
    using System;

    /// <summary>
    /// Using console for interaction between user and program
    /// </summary>
    internal class ConsoleInputOutputDevice : Interfaces.IInputOutputDevice
    {
        /// <summary>
        /// Read data line from console
        /// </summary>
        /// <returns>Data from console</returns>
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Set cursor position
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y Coordinate</param>
        public void SetPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        /// <summary>
        /// Print in console and line break
        /// </summary>
        /// <param name="dataToOutPut">Data string for output</param>
        public void WriteLineOutput(string dataToOutPut)
        {
            Console.WriteLine(dataToOutPut);
        }

        /// <summary>
        /// Print in console without line break
        /// </summary>
        /// <param name="dataToOutPut">Data string for output</param>
        public void WriteOutput(string dataToOutPut)
        {
            Console.Write(dataToOutPut);
        }

        /// <summary>
        /// Clear screen in console
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }
    }
}
