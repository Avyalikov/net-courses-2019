// <copyright file="IInputOutputDevice.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    /// <summary>
    /// Input and output device interface
    /// </summary>
    public interface IInputOutputDevice
    {
        /// <summary>
        /// Read input method
        /// </summary>
        /// <returns>user input</returns>
        string ReadInput();

        /// <summary>
        /// ReadKey method
        /// </summary>
        /// <returns>Key char</returns>
        char ReadKey();

        /// <summary>
        /// Write output method
        /// </summary>
        /// <param name="dataToOutput">Data to output</param>
        void WriteOutput(string dataToOutput);
    }
}
