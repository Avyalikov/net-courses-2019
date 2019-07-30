namespace Console_Draw_Game
{
    class Figures : Interfaces.IFigures
    {
        public void Dot(Interfaces.IBoard board)
        {
            board.Draw('.', board.BoardSizeX / 2, board.BoardSizeY / 2);
        }

        public void HorizontalLine(Interfaces.IBoard board)
        {
            for (int i = 1; i < board.BoardSizeX - 1; i++)
            {
                board.Draw('-', i, (board.BoardSizeY / 2) + 2);
            }
        }

        public void VerticalLine(Interfaces.IBoard board)
        {
            for (int i = 1; i < board.BoardSizeY - 1; i++)
            {
                board.Draw('|', (board.BoardSizeY / 2) + 2, i);
            }
        }

        public void SharpLine(Interfaces.IBoard board)
        {
            for (int i = 1; i < board.BoardSizeY - 1; i++)
            {
                int coordX = board.BoardSizeX - (i * 2);
                if (coordX > 1)
                {
                    board.Draw('#', coordX, i);
                }
            }
        }
    }
}
