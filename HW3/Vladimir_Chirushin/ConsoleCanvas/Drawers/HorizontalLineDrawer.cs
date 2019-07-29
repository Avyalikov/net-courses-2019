namespace ConsoleCanvas.Drawers
{
    using ConsoleCanvas.Interfaces;

    public class HorizontalLineDrawer : IObjectDrawer
    {
        private const string HorizontalSymbol = "-";
        private const string CornerSymbol = "+";

        private readonly IDrawManager drawManager;
        private readonly int yOffsetPercent;

        public HorizontalLineDrawer(IDrawManager drawManager, int yOffsetPercent)
        {
            this.drawManager = drawManager;
            this.yOffsetPercent = yOffsetPercent;
        }

        public void DrawObject(IBoard board)
        {
            int lineYPos = board.Y1 + (board.BoardSizeY * this.yOffsetPercent / 100);

            for (int i = board.X1; i < board.X2; i++)
            {
                this.drawManager.WriteAt(HorizontalSymbol, i, lineYPos);
            }

            // drawing fancy ends
            this.drawManager.WriteAt(CornerSymbol, board.X1, lineYPos);
            this.drawManager.WriteAt(CornerSymbol, board.X2, lineYPos);
        }
    }
}