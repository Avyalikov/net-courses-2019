namespace TradingSimulator.Components
{
    using Core.Interfaces;
    using Core.Model;
    using System;

    public class ConsoleIO : IInputOutput
    {
        public Point CursorPosition
        {
            get => (Console.CursorLeft, Console.CursorTop);
            set => Console.SetCursorPosition(value.x, value.y);
        }

        public void SetWindowSize(int width, int height)
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        public string Input() => Console.ReadLine();

        public void Print(string str) => Console.Write(str);

        public void Clear(Point TopLeft, Point BottomRight)
        {
            for (int y = TopLeft.y; y < BottomRight.y; y++)
            {
                for (int x = TopLeft.x; x < BottomRight.x; x++)
                {
                    this.CursorPosition = (x, y);
                    this.Print(" ");
                }
            }
        }
    }
}