using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This class represents Console wrapper for our game.
    /// </summary>
    class GameConsole : IInputOutputDevice
    {
        /// <summary>
        /// This method reads a line from Console.
        /// </summary>
        /// <returns>String from input.</returns>
        string IInputOutputDevice.Read()
        {
            return Console.ReadLine();
        }
        /// <summary>
        /// This method writes a message in the Console using green font.
        /// </summary>
        /// <param name="msg">String message for console.</param>
        void IInputOutputDevice.Write(string msg)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write(msg);

            Console.ForegroundColor = temp;
        }
        /// <summary>
        /// This method writes an error message in the Console using red font.
        /// </summary>
        /// <param name="msg">String error message for console.</param>
        void IInputOutputDevice.WriteError(string msg)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(msg);
            Console.WriteLine();

            Console.ForegroundColor = temp;
        }
        /// <summary>
        /// This method writes a message in the Console using green font adding the current line terminator.
        /// </summary>
        /// <param name="msg">String message for console.</param>
        void IInputOutputDevice.WriteLine(string msg)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(msg);

            Console.ForegroundColor = temp;
        }
        /// <summary>
        /// This method writes the current line terminator to the Console.
        /// </summary>
        void IInputOutputDevice.WriteLine()
        {
            Console.WriteLine();
        }
    }
}
