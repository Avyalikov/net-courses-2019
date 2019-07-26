namespace GameWhichCanDraw.Interfaces
{
    public interface IBoard
    {
        int BoardSizeX { get; set; }

        int BoardSizeY { get; set; }

        void Create();

        void WriteAt(char c, int x, int y);
    }
}
