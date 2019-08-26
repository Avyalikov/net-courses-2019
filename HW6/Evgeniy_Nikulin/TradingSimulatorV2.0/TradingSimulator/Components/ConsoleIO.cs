namespace TradingSimulator.Components
{
    using System;
    using Core.Dto;
    using Core.Interfaces;

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
            string str = string.Empty;

            for (int i = TopLeft.x; i < BottomRight.x; i++)
            {
                str += " ";
            }

            for (int i = TopLeft.y; i < BottomRight.y; i++)
            {
                this.CursorPosition = (TopLeft.x, i);
                this.Print(str);
            }
        }
    }
}