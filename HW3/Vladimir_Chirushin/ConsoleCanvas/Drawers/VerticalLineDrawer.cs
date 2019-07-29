namespace ConsoleCanvas.Drawers
{
    using ConsoleCanvas.Interfaces;

    public class VerticalLineDrawer : IObjectDrawer
    {
        private const string VerticalSymbol = "|";
        private const string CornerSymbol = "+";

        private readonly IDrawManager drawManager;
        private readonly int xOffsetPercent;

        public VerticalLineDrawer(IDrawManager drawManager, int xOffsetPercent)
        {
            this.drawManager = drawManager;
            this.xOffsetPercent = xOffsetPercent;
        }

        public void DrawObject(IBoard board)
        {
            // TODO:cvhange to size
            int lineXPos = board.X1 + (board.BoardSizeX * this.xOffsetPercent / 100);

            for (int i = board.Y1; i < board.Y2; i++)
            {
                this.drawManager.WriteAt(VerticalSymbol, lineXPos, i);
            }

            // drawing fancy ends
            this.drawManager.WriteAt(CornerSymbol, lineXPos, board.Y1);
            this.drawManager.WriteAt(CornerSymbol, lineXPos, board.Y2);
        }
    }
}