namespace ConsoleDrawGame.Classes
{
    using System;
    using ConsoleDrawGame.Interfaces;

    internal class ConsoleInputOutput : IInputOutput
    {
        private int origCol;
        private int origRow;

        public ConsoleInputOutput()
        {
            this.ClearConsole();
            this.origCol = Console.CursorLeft;
            this.origRow = Console.CursorTop;
        }

        /// <summary>Clears the console.</summary>
        public void ClearConsole()
        {
            Console.Clear();
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

        /// <summary>Prints a string on X and Y coordinates in console.</summary>
        /// <param name="s">String to print.</param>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(this.origCol + x, this.origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void SetCursor(int x, int y)
        {
            Console.CursorLeft = x;
            Console.CursorTop = y;

            this.origCol = x;
            this.origRow = y;
        }
    }
}
