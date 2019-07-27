namespace ConsoleCanvas
{
    public interface IDrawManager
    {
        void DrawInitiate();
        void ProceedDrawing(DrawDelegate drawDelegat, Canvas canvas);
        void WriteAt(string userString, int x, int y);
        void WriteLine(string outputString);
    }
}