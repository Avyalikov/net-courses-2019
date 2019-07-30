namespace HW3
{
    public interface ICommands
    {
        void DrawDashboard(IBoard board);

        void DrawDot(IBoard board);

        void DrawHorizontalLine(IBoard board);

        void DrawVerticalLine(IBoard board);

        void DrawSnowFlake(IBoard board);
    }
}
