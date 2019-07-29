namespace ConsoleCanvas
{
    using System;
    using System.Linq;
    using ConsoleCanvas.Interfaces;

    public class ConsoleDrawManager : IDrawManager
    {
        private int origRow;
        private int origCol;
        private bool isInitialized = false;

        public void Initialize()
        {
            if (this.isInitialized)
            {
                return;
            }

            Console.Clear();
            this.origRow = Console.CursorTop;
            this.origCol = Console.CursorLeft;
            this.isInitialized = true;
        }

        public void WriteAt(string userString, int x, int y)
        {
            try
            {
                this.Initialize();
                Console.SetCursorPosition(this.origCol + x, this.origRow + y);
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
            this.Initialize();
            Console.Clear();
            if (drawDelegate != null)
            {
                drawDelegate(board);
                this.WriteAt($"There is {drawDelegate.GetInvocationList().Count().ToString()} objects on canvas!", 0, 28);
            }
            else
            {
                this.WriteAt($"Canvas is clean!", 0, 28);
            }
        }

        public void WriteLine(string outputString)
        {
            this.Initialize();
            Console.WriteLine(outputString);
        }
    }
}