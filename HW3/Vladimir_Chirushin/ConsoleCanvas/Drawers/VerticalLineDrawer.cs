using ConsoleCanvas.Interfaces;

namespace ConsoleCanvas.Drawers
{
    public class VerticalLineDrawer : IObjectDrawer
    {
        private readonly IDrawManager drawManager;
        private readonly int xOffsetPercent;
        private const string verticalSymbol = "|";
        private const string cornerSymbol = "+";

        public VerticalLineDrawer(IDrawManager drawManager, int xOffsetPercent)
        {
            this.drawManager = drawManager;
            this.xOffsetPercent = xOffsetPercent;
        }

        public void DrawObject(IBoard board)
        {
            // TODO:cvhange to size
            int lineXPos = board.x1 + (board.BoardSizeX * xOffsetPercent / 100);

            for (int i = board.y1; i < board.y2; i++)
            {
                drawManager.WriteAt(verticalSymbol, lineXPos, i);
            }

            // drawing fancy ends
            drawManager.WriteAt(cornerSymbol, lineXPos, board.y1);     
            drawManager.WriteAt(cornerSymbol, lineXPos, board.y2);
        }
    }
}

