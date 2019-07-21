using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame
{
    class GameConsole : IInputOutputDevice
    {

        string IInputOutputDevice.Read()
        {
            return Console.ReadLine();
        }

        void IInputOutputDevice.Write(string msg)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write(msg);

            Console.ForegroundColor = temp;
        }

        void IInputOutputDevice.WriteError(string msg)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(msg);

            Console.ForegroundColor = temp;
        }

        void IInputOutputDevice.WriteLine(string msg)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(msg);

            Console.ForegroundColor = temp;
        }

        void IInputOutputDevice.WriteLine()
        {
            Console.WriteLine();
        }
    }
}
