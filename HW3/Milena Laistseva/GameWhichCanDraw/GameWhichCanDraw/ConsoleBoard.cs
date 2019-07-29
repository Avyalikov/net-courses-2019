using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWhichCanDraw
{
    public class ConsoleBoard: IBoard
    {
        private int sizeX;
        private int sizeY;

        public int boardSizeX
        {
            get { return sizeX; }
            set
            {
                if (value < 2 || value > 60)
                    sizeX = 60;
                else
                    sizeX = value;
            }
        }

        public int boardSizeY
        {
            get { return sizeY; }
            set
            {
                if (value < 2 || value > 20)
                    sizeY = 20;
                else
                    sizeY = value;
            }
        }

        public void WriteAt (string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void DrawBoard(IBoard board)
        {
            WriteAt("+", 0, 0);
            for (int i = 1; i < boardSizeY; i++)
            {
                WriteAt("|", 0, i);
            }
            WriteAt("+", 0, boardSizeY);

            for(int i = 1; i < boardSizeX; i++)
            {
                WriteAt("-", i, boardSizeY);
            }
            WriteAt("+", boardSizeX, boardSizeY);

            for (int i = boardSizeY - 1; i > 0; i--)
            {
                WriteAt("|", boardSizeX, i);
            }
            WriteAt("+", boardSizeX, 0);

            for (int i = boardSizeX - 1; i > 0; i--)
            {
                WriteAt("-", i, 0);
            }
        }        
    }
}
