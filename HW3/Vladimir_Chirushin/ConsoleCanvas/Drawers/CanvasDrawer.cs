namespace ConsoleCanvas.Drawers
{
    using ConsoleCanvas.Interfaces;

    public class CanvasDrawer : IObjectDrawer
    {
        private const string HorizontalSymbol = "-";
        private const string VerticalSymbol = "|";
        private const string CornerSymbol = "+";

        private readonly IDrawManager drawManager;

        public CanvasDrawer(IDrawManager drawManager)
        {
            this.drawManager = drawManager;
        }

        public void DrawObject(IBoard board)
        {
            // drawing horizontal lines
            for (int i = board.X1; i < board.X2; i++)
            {
                this.drawManager.WriteAt(HorizontalSymbol, i, board.Y1);
                this.drawManager.WriteAt(HorizontalSymbol, i, board.Y2);
            }

            // drawing vertical lines
            for (int i = board.Y1; i < board.Y2; i++)
            {
                this.drawManager.WriteAt(VerticalSymbol, board.X1, i);
                this.drawManager.WriteAt(VerticalSymbol, board.X2, i);
            }

            // drawing fancy corners
            this.drawManager.WriteAt(CornerSymbol, board.X1, board.Y1);
            this.drawManager.WriteAt(CornerSymbol, board.X1, board.Y2);
            this.drawManager.WriteAt(CornerSymbol, board.X2, board.Y1);
            this.drawManager.WriteAt(CornerSymbol, board.X2, board.Y2);
        }
    }
}