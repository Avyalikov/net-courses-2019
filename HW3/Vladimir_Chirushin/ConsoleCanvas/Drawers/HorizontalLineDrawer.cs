using ConsoleCanvas.Interfaces;

namespace ConsoleCanvas.Drawers
{
    public class HorizontalLineDrawer : IObjectDrawer
    {
        private readonly IDrawManager drawManager;
        private readonly int yOffsetPercent;
        private const string horizontalSymbol = "-";
        private const string cornerSymbol = "+";

        public HorizontalLineDrawer(IDrawManager drawManager, int yOffsetPercent)
        {
            this.drawManager = drawManager;
            this.yOffsetPercent = yOffsetPercent;
        }

        public void DrawObject(IBoard board)
        {
            int lineYPos = board.y1 + (board.BoardSizeY * yOffsetPercent / 100);

            for (int i = board.x1; i < board.x2; i++)
            {
                drawManager.WriteAt(horizontalSymbol, i, lineYPos);
            }

            // drawing fancy ends
            drawManager.WriteAt(cornerSymbol, board.x1, lineYPos);     
            drawManager.WriteAt(cornerSymbol, board.x2, lineYPos);
        }
    }
}