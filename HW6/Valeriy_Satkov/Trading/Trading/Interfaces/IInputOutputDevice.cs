// <copyright file="IInputOutputDevice.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace Trading.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Using device for interaction between user and program
    /// </summary>
    interface IInputOutputDevice
    {
        /// <summary>
        /// Read data from source
        /// </summary>
        /// <returns>Data from source</returns>
        string ReadLine();

        /// <summary>
        /// Send data and line break to source
        /// </summary>
        /// <param name="dataToOutPut">Data string for output</param>
        void WriteLine(string dataToOutPut);

        /// <summary>
        /// Send data without line break to source
        /// </summary>
        /// <param name="dataToOutPut">Data string for output</param>
        void Write(string dataToOutPut);

        /// <summary>
        /// Set cursor position
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y Coordinate</param>
        void SetCursorPosition(int x, int y);

        /// <summary>
        /// Clear output screen
        /// </summary>
        void Clear();
    }
}
