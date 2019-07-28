using ConsoleCanvas.Interfaces;

namespace ConsoleCanvas.Drawers
{
    public class CanvasDrawer : IObjectDrawer
    {
        private readonly IDrawManager drawManager;
        private const string horizontalSymbol = "-";
        private const string verticalSymbol = "|";
        private const string cornerSymbol = "+";

        public CanvasDrawer(IDrawManager drawManager)
        {
            this.drawManager = drawManager;
        }

        public void DrawObject(IBoard board)
        {
            // drawing horizontal lines
            for (int i = board.x1; i < board.x2; i++)
            {
                drawManager.WriteAt(horizontalSymbol, i, board.y1);
                drawManager.WriteAt(horizontalSymbol, i, board.y2);
            }

            // drawing vertical lines
            for (int i = board.y1; i < board.y2; i++)
            {
                drawManager.WriteAt(verticalSymbol, board.x1, i);
                drawManager.WriteAt(verticalSymbol, board.x2, i);
            }

            // drawing fancy corners
            drawManager.WriteAt(cornerSymbol, board.x1, board.y1);
            drawManager.WriteAt(cornerSymbol, board.x1, board.y2);
            drawManager.WriteAt(cornerSymbol, board.x2, board.y1);
            drawManager.WriteAt(cornerSymbol, board.x2, board.y2);
        }
    }
}