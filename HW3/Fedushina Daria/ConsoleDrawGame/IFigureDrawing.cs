namespace ConsoleDrawGame
{
    internal interface IFigureDrawing
    {
        void DrawDot(IBoard board);
        void DrawHorisontalLine(IBoard board);
        void DrawVerticalLine(IBoard board);
        void DrawSquare(IBoard board);

    }
}