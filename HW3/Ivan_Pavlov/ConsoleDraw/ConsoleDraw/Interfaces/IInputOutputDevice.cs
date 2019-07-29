// <copyright file="IInputOutputDevice.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw.Interfaces
{
    /// <summary>
    /// IO interface.
    /// </summary>
    public interface IInputOutputDevice
    {
        /// <summary>
        /// read user input.
        /// </summary>
        /// <returns>user's input.</returns>
        string ReadInput();

        /// <summary>
        /// set cursor on start position.
        /// </summary>
        /// <param name="x"> x-coordinate.</param>
        /// <param name="y"> y-coordinate.</param>
        void SetPosition(int x, int y);

        /// <summary>
        /// output to user.
        /// </summary>
        /// <param name="dataToOutPut">info.</param>
        void WriteLineOutput(string dataToOutPut);

        /// <summary>
        /// output to user.
        /// </summary>
        /// <param name="dataToOutPut">info.</param>
        void WriteOutput(string dataToOutPut);

        /// <summary>
        /// clear console.
        /// </summary>
        void Clear();
    }
}
