namespace ConsoleCanvas.Interfaces
{
    public interface IBoard
    {
        int BoardSizeX { get; }

        int BoardSizeY { get; }

        int X1 { get; } // upper left corner

        int Y1 { get; }

        int X2 { get; } // bottom right corner

        int Y2 { get; }
    }
}
