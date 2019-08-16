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
        string ReadInput();

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
    }
}
