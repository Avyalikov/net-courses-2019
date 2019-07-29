// <copyright file="ConsoleIO.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw.Provider
{
    using System;
    using ConsoleDraw.Interfaces;

    /// <summary>
    /// console provider class.
    /// </summary>
    internal class ConsoleIO : IInputOutputDevice
    {
        /// <inheritdoc/>
        /// clear the consol
        public void Clear()
        {
            Console.Clear();
        }

        /// <inheritdoc/>
        /// return users's inpuut
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        /// <inheritdoc/>
        /// set posittion on console
        public void SetPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        /// <inheritdoc/>
        /// write output to user
        public void WriteLineOutput(string dataToOutPut)
        {
            Console.WriteLine(dataToOutPut);
        }

        /// <inheritdoc/>
        /// write output to user
        public void WriteOutput(string dataToOutPut)
        {
            Console.Write(dataToOutPut);
        }
    }
}
