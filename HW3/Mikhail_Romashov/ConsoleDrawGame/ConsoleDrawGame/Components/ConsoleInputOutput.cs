//-----------------------------------------------------------------------
// <copyright file="ConsoleInputOutput.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Components
{
    using System;
    using Interfaces;

    /// <summary>
    /// Class for work with input output to screen
    /// </summary>
    public class ConsoleInputOutput : IInputOutput
    {
        /// <summary>
        /// Get string from input information
        /// </summary>
        /// <returns>String from input information</returns>
        public string ReadInputLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Get char from input information
        /// </summary>
        /// <returns>Char from input information</returns>
        public char ReadInputKey()
        {
            return Console.ReadKey().KeyChar;
        }

        /// <summary>
        /// Show line to screen
        /// </summary>
        /// <param name="dataToOutput">Line to screen</param>
        public void WriteOutputLine(string dataToOutput)
        {
            Console.WriteLine(dataToOutput);
        }

        /// <summary>
        /// Show empty line to screen
        /// </summary>
        public void WriteOutputLine()
        {
            Console.WriteLine();
        }

        /// <summary> 
        /// Show line to screen
        /// </summary>
        /// <param name="dataToOutput">Line to screen</param>
        public void WriteOutput(string dataToOutput)
        {
            Console.Write(dataToOutput);
        }

        /// <summary> 
        /// Clear screen
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Set screen position for output line
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void CursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
    }
}