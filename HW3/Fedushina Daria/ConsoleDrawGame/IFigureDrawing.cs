namespace ConsoleDrawGame
{
    internal interface IFigureDrawing
    {
        int Bender { get; set; }
        void DrawDot(IBoard board);
        void DrawHorisontalLine(IBoard board);
        void DrawVerticalLine(IBoard board);
        void DrawSquare(IBoard board);

    }
}