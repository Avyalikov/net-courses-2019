using System;
using System.Linq;

namespace ConsoleCanvas
{
    public class DrawManager : IDrawManager
    {
        protected static int origRow;
        protected static int origCol;
        public void DrawInitiate()
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
        }
        public void WriteAt(string userString, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(userString);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }


        public void ProceedDrawing(DrawDelegate drawDelegat, Canvas canvas)
        {
            Console.Clear();
            if (drawDelegat != null)
            {
                drawDelegat(canvas);
                WriteAt($"There is {drawDelegat.GetInvocationList().Count().ToString()} objects on canvas!", 0, 28);
            }
            else
            {
                WriteAt($"Canvas is clean!", 0, 28);
            }
        }
    }
}

