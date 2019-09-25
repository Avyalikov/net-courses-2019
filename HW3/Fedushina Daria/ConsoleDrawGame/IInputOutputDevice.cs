namespace ConsoleDrawGame
{
    internal interface IInputOutputDevice
    {
        void WriteOutput(string output);

        void WriteWithStayOnLine(string output);
        string ReadOutput();
        void Clear();
        void ClearRow(int y);

        void SetCursorPosition(int x, int y);

        (int,int) GetCursorPosition();
    }
}