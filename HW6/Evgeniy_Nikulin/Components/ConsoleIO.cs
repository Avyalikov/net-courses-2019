namespace Components
{
    using System;
    using Core.Interfaces;
    using Core.Model;

    public class ConsoleIO : IInputOutput
    {
        public void SetWindowSize(int width, int height)
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        public string Input() => Console.ReadLine();

        public void Print(string str) => Console.Write(str);

        public void Print(string str, int StartX, int StartY)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Console.SetCursorPosition(StartX, StartY);
            this.Print(str);
            Console.SetCursorPosition(x, y);
        }

        public void Clear(int LeftTopX, int LeftTopY, int BottomRightX, int BottomRightY)
        {
            for (int y = LeftTopY; y < BottomRightY; y++)
            {
                for (int x = LeftTopX; x < BottomRightX; x++)
                {
                    this.Print(" ", x, y);
                }
            }
        }

        public void Print(Transaktion trn, int StartX, int StartY)
        {
            //$"{sellerName} {sellerSurname} sells {quantity} quantity of {shareName} shares to {buyerName} {buyerSurname}";
            int x = Console.CursorLeft;
            int y = Console.CursorTop;


            Console.SetCursorPosition(x, y);
        }
    }
}