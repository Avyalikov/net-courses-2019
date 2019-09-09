// <copyright file="ConsoleIODevice.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace Trading
{
    using System;

    /// <summary>
    /// Defines the <see cref="ConsoleIODevice" />
    /// </summary>
    internal class ConsoleIODevice : IInputOutputDevice
    {
        /// <summary>
        /// The InputValue
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string InputValue()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// The Print
        /// </summary>
        /// <param name="printValue">The printValue<see cref="string"/></param>
        public void Print(string printValue)
        {
            Console.WriteLine(printValue);
        }

        /// <summary>
        /// The KeyInput
        /// </summary>
        /// <returns>The <see cref="char"/></returns>
        public char KeyInput()
        {
            return Console.ReadKey().KeyChar;
        }
    }
}
