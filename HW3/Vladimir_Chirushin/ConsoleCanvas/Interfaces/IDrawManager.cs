namespace ConsoleCanvas.Interfaces
{
    public interface IDrawManager
    {
        void Initialize();

        void Draw(DrawDelegate drawDelegate, IBoard canvas);

        void WriteAt(string userString, int x, int y);

        void WriteLine(string outputString);
    }
}