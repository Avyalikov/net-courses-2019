namespace HW3
{
    interface ICommands
    {
        void DrawDashboard(IBoard board);
        void DrawDot(IBoard board);
        void DrawHorizontalLine(IBoard board);
        void DrawVerticalLine(IBoard board);
        void DrawSnowFlake(IBoard board);
    }
}
