using ConsoleCanvas.Interfaces;

namespace ConsoleCanvas
{
    public class Board : IBoard
    {
        public int BoardSizeX { get { return x2 - x1; } }
        public int BoardSizeY { get { return y2 - y1; } }

        public int x1 { get; }  // upper left corner
        public int y1 { get; }
        public int x2 { get; }  // bottom right corner
        public int y2 { get; }

        public Board(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;

            this.x2 = x2;
            this.y2 = y2;
        }
    }
}
