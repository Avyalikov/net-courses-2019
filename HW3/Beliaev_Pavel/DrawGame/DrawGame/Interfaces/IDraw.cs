namespace DrawGame.Interfaces
{
    internal interface IDraw
    {
        void DrawBoard(IBoard board);

        void DrawSimpleDot(IBoard board);

        void DrawVerticalLine(IBoard board);

        void DrawHorizontalLine(IBoard board);

        void DrawDiamond(IBoard board);

        void ClearConsole(IBoard board);
    }
}
