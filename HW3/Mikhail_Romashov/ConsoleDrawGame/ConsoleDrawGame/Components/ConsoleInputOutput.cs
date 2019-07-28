//-----------------------------------------------------------------------
// <copyright file="ConsoleInputOutput.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ConsoleDrawGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for work with input output to screen
    /// </summary>
    public class ConsoleInputOutput : Interfaces.IInputOutput
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
    }
}