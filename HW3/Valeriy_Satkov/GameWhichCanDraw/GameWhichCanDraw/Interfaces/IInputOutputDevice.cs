// <copyright file="IInputOutputDevice.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw.Interfaces
{
    /// <summary>
    /// Using device for interaction between user and program
    /// </summary>
    public interface IInputOutputDevice
    {
        /// <summary>
        /// Read data from source
        /// </summary>
        /// <returns>Data from source</returns>
        string ReadInput();

        /// <summary>
        /// Set cursor position
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y Coordinate</param>
        void SetPosition(int x, int y);

        /// <summary>
        /// Send data and line break to source
        /// </summary>
        /// <param name="dataToOutPut">Data string for output</param>
        void WriteLineOutput(string dataToOutPut);

        /// <summary>
        /// Send data without line break to source
        /// </summary>
        /// <param name="dataToOutPut">Data string for output</param>
        void WriteOutput(string dataToOutPut);

        /// <summary>
        /// Clear output screen
        /// </summary>
        void Clear();
    }
}
