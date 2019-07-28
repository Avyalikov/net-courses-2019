using ConsoleCanvas.Interfaces;
using System;
using System.Linq;

namespace ConsoleCanvas
{
    public class ConsoleDrawManager : IDrawManager
    {
        private int origRow;
        private int origCol;
        private bool isInitialized = false;

        public void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            isInitialized = true;
        }

        public void WriteAt(string userString, int x, int y)
        {
            try
            {
                Initialize();
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(userString);
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void Draw(DrawDelegate drawDelegate, IBoard board)
        {
            Initialize();
            Console.Clear();
            if (drawDelegate != null)
            {
                drawDelegate(board);
                WriteAt($"There is {drawDelegate.GetInvocationList().Count().ToString()} objects on canvas!", 0, 28);
            }
            else
            {
                WriteAt($"Canvas is clean!", 0, 28);
            }
        }

        public void WriteLine(string outputString)
        {
            Initialize();
            Console.WriteLine(outputString);
        }
    }
}