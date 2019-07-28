using ConsoleCanvas.Interfaces;

namespace ConsoleCanvas.Drawers
{
    public class DotDrawer : IObjectDrawer
    {
        private readonly IDrawManager drawManager;
        private readonly int xOffsetPercent;
        private readonly int yOffsetPercent;
        private const string dotSymbol = ".";

        public DotDrawer(IDrawManager drawManager, int dotXOffsetPercent, int dotYOffsetPercent)
        {
            this.drawManager = drawManager;
            this.xOffsetPercent = dotXOffsetPercent;
            this.yOffsetPercent = dotYOffsetPercent;
        }

        public void DrawObject(IBoard board)
        {
            int dotXPos = board.x1 + (board.BoardSizeX * xOffsetPercent / 100);
            int dotYPos = board.y1 + (board.BoardSizeY * yOffsetPercent / 100);

            drawManager.WriteAt(dotSymbol, dotXPos, dotYPos);
        }
    }
}
