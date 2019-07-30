namespace DrawGame
{
    using System;
    using DrawGame.Interfaces;

    internal class DrawOnBoard : IDraw
    {
        public void DrawBoard(IBoard board)
        {
            this.Draw("+", 0, 0);
            for (int i = 1; i < board.BoardSizeX; i++)
            {
                this.Draw("-", i, 0);
            }

            this.Draw("+", board.BoardSizeX, 0);
            for (int i = 1; i < board.BoardSizeY; i++)
            {
                this.Draw("|", board.BoardSizeX, i);
            }

            this.Draw("+", board.BoardSizeX, board.BoardSizeY);
            for (int i = board.BoardSizeX - 1; i > 0; i--)
            {
                this.Draw("-", i, board.BoardSizeY);
            }

            this.Draw("+", 0, board.BoardSizeY);
            for (int i = board.BoardSizeY - 1; i > 0; i--)
            {
                this.Draw("|", 0, i);
            }

            Console.SetCursorPosition(0, board.BoardSizeY + 2);
        }
        
        public void DrawSimpleDot(IBoard board)
        {
            this.Draw(".", board.BoardSizeX / 2, board.BoardSizeY / 2);
            Console.SetCursorPosition(0, board.BoardSizeY + 2);
        }
        
        public void DrawVerticalLine(IBoard board)
        {                       
            for (int i = 1; i < board.BoardSizeY; i++)
            {
                this.Draw("|", board.BoardSizeX / 2, i);
            }

            Console.SetCursorPosition(0, board.BoardSizeY + 2);
        }
        
        public void DrawHorizontalLine(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeX; i++)
            {
                this.Draw("-", i, board.BoardSizeY / 2);
            }

            Console.SetCursorPosition(0, board.BoardSizeY + 2);
        }
        
        public void DrawDiamond(IBoard board)
        {
            int rombSize = Math.Min(board.BoardSizeY, board.BoardSizeX);                

            for (int i = 1; i < (rombSize / 2); i++)
            {
                this.Draw("/", ((board.BoardSizeX - rombSize) / 2) + i, (board.BoardSizeY / 2) - i);
            }

            for (int i = 1; i < (rombSize / 2); i++)
            {
                this.Draw("\\", (board.BoardSizeX / 2) + i, ((board.BoardSizeY - rombSize) / 2) + i);
            }
            
            for (int i = 1; i < (rombSize / 2); i++)
            {
                this.Draw("/", ((board.BoardSizeX + rombSize) / 2) - i, (board.BoardSizeY / 2) + i);
            }
            
            for (int i = 1; i < (rombSize / 2); i++)
            {
                this.Draw("\\", (board.BoardSizeX / 2) - i, ((board.BoardSizeY + rombSize) / 2) - i);
            }

            Console.SetCursorPosition(0, board.BoardSizeY + 2);
        }
        
        public void ClearConsole(IBoard board)
        {
            Console.Clear();
        }

        private void Draw(string s, int x, int y)
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
    }
}