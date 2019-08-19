namespace TradingSimulator.Interfaces
{
    using System.Collections;
    public interface IInputOutput
    {
        string Input();
        void Print(string sep);
        void Print(string str, int StartX, int StartY);
        void Clear(int LeftTopX, int LeftTopY, int BottomRightX, int BottomRightY);
    }
}