using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame
{
    class ConsoleIO : IInputOutputDevice
    {
        public string ReadOutput()
        {
            return Console.ReadLine();
        }

        public void WriteWithStayOnLine(string output)
        {
            Console.Write(output);
        }

        public void WriteOutput(string output)
        {
            Console.WriteLine(output);
        }

        public void Clear()
        {
            Console.Clear();
        }
        public void ClearRow(int y)                         // (c) by Svetlana Koroleva
        {
            StringBuilder s = new StringBuilder();
            s.Length = 50;// or add int parametr to method 
            Console.ForegroundColor = ConsoleColor.Black;
            this.SetCursorPosition(0, y);
            this.WriteOutput(s.ToString());
            this.SetCursorPosition(0, y);
            Console.ForegroundColor = ConsoleColor.White;

        }

        public void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
        public (int,int) GetCursorPosition()
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            return (x,y);
        }
    }
}
