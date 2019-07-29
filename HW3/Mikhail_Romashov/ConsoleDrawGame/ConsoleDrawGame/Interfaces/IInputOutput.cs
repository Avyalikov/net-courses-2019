//-----------------------------------------------------------------------
// <copyright file="IInputOutput.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Interfaces
{
    /// <summary>
    /// Interface for work with input output to screen
    /// </summary>
    public interface IInputOutput
    {
        /// <summary>
        /// Get string from input information
        /// </summary>
        /// <returns>String from input information</returns>
        string ReadInputLine();

        /// <summary>
        /// Get char from input information
        /// </summary>
        /// <returns>Char from input information</returns>
        char ReadInputKey();

        /// <summary>
        /// Show line to screen
        /// </summary>
        /// <param name="dataToOutput">Line to screen</param>
        void WriteOutputLine(string dataToOutput);

        /// <summary>
        /// Show empty line to screen
        /// </summary>
        void WriteOutputLine();

        /// <summary> 
        /// Show line to screen
        /// </summary>
        /// <param name="dataToOutput">Line to screen</param>
        void WriteOutput(string dataToOutput);

        /// <summary> 
        /// Clear screen
        /// </summary>
        void Clear();

        /// <summary>
        /// Set screen position for output line
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        void CursorPosition(int x, int y);
    }
}