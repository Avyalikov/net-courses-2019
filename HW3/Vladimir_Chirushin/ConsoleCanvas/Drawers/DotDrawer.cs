namespace ConsoleCanvas.Drawers
{
    using ConsoleCanvas.Interfaces;

    public class DotDrawer : IObjectDrawer
    {
        private const string DotSymbol = ".";

        private readonly IDrawManager drawManager;
        private readonly int xOffsetPercent;
        private readonly int yOffsetPercent;

        public DotDrawer(IDrawManager drawManager, int dotXOffsetPercent, int dotYOffsetPercent)
        {
            this.drawManager = drawManager;
            this.xOffsetPercent = dotXOffsetPercent;
            this.yOffsetPercent = dotYOffsetPercent;
        }

        public void DrawObject(IBoard board)
        {
            int dotXPos = board.X1 + (board.BoardSizeX * this.xOffsetPercent / 100);
            int dotYPos = board.Y1 + (board.BoardSizeY * this.yOffsetPercent / 100);

            this.drawManager.WriteAt(DotSymbol, dotXPos, dotYPos);
        }
    }
}
