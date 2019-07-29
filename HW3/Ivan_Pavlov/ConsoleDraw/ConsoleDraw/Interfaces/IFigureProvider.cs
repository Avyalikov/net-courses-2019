namespace ConsoleDraw.Interfaces
{
    public interface IFigureProvider
    {
        void Dot(IBoard board);

        void VerticalLine(IBoard board);

        void HorizontalLine(IBoard board);

        void Curve(IBoard board);
    }
}
