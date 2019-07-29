using ConsoleDrawGame.Interfaces;
using System;

namespace ConsoleDrawGame.Classes
{
    class ConsoleInputOutput : IInputOutput
    {
        private int origRow;
        private int origCol;

        public ConsoleInputOutput()
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
        }

        /// <summary>Returns string from console</summary>
        /// <returns></returns>
        public string ReadInput()
        {
            return Console.ReadLine();
        }
        /// <summary>Returns a char from Console input.</summary>
        /// <returns></returns>
        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }
        /// <summary>Prints data in Console.</summary>
        /// <param name="dataToOutput">String to print.</param>
        public void WriteOutput(string dataToOutput)
        {
            Console.Write(dataToOutput);
        }
        /// <summary>Prints a string on X and Y coordinates in concole.</summary>
        /// <param name="s">String to print.</param>
        /// <param name="x">X coord.</param>
        /// <param name="y">Y coord.</param>
        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
