namespace GameWhichCanDraw.Interfaces
{
    using System;
    using Interfaces;

    public interface IFigureProvider
    {
        void SimpleDot(IBoard board);

        void VerticalLine(IBoard board);

        void HorizontalLine(IBoard board);

        void Curve(IBoard board);
    }
}
