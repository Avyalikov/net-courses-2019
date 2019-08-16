// <copyright file="ConsoleInputOutputDevice.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace Trading.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Using console for interaction between user and program
    /// </summary>
    class ConsoleInputOutputDevice : Interfaces.IInputOutputDevice
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
    }
}
