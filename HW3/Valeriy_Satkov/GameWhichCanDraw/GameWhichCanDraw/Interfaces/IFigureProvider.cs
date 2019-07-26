namespace GameWhichCanDraw.Interfaces
{
    using System;
    using Interfaces;

    interface IFigureProvider
    {
        void SimpleDot(IBoard board);
        void VerticalLine(IBoard board);
        void HorizontalLine(IBoard board);
        void Curve(IBoard board);
        void Empty(IBoard board);
    }
}
