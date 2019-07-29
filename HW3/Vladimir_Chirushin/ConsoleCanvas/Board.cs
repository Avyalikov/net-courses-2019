namespace ConsoleCanvas
{
    using ConsoleCanvas.Interfaces;

    public class Board : IBoard
    {
        public Board(int x1, int y1, int x2, int y2)
        {
            this.X1 = x1;
            this.Y1 = y1;

            this.X2 = x2;
            this.Y2 = y2;
        }

        public int BoardSizeX
        {
            get { return this.X2 - this.X1; }
        }

        public int BoardSizeY
        {
            get { return this.Y2 - this.Y1; }
        }

        public int X1 { get; }  // upper left corner

        public int Y1 { get; }

        public int X2 { get; }  // bottom right corner

        public int Y2 { get; }
    }
}
