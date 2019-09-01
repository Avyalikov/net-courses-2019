namespace TradingSimulator.Core.Interfaces
{
    using Dto;
    public interface IInputOutput
    {
        Point CursorPosition { get; set; }
        void SetWindowSize(int width, int height);
        string Input();
        void Print(string sep);
        void Clear(Point TopLeft, Point BottomRight);
    }
}