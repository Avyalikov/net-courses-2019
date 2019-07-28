namespace ConsoleCanvas.Interfaces
{
    public interface IBoard
    {
        int BoardSizeX { get; }
        int BoardSizeY { get; }

        int x1 { get; }  //upper left corner
        int y1 { get; }
        int x2 { get; }  //bottom right corner
        int y2 { get; }
    }
}
