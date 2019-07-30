// <copyright file="ConsoleInputOutputDevice.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    using System;

    /// <summary>
    /// Console input and output device 
    /// </summary>
    public class ConsoleInputOutputDevice : IInputOutputDevice
    {
        /// <summary>
        /// Reads input data from console
        /// </summary>
        /// <returns>Console input data</returns>
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Obtains pressed key
        /// </summary>
        /// <returns>key char</returns>
        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }

        /// <summary>
        /// Writes output data to console
        /// </summary>
        /// <param name="dataToOutput">Output data</param>
        public void WriteOutput(string dataToOutput)
        {
            Console.WriteLine(dataToOutput);
        }
    }
}
