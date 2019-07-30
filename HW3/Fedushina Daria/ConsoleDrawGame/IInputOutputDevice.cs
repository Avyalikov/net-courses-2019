namespace ConsoleDrawGame
{
    internal interface IInputOutputDevice
    {
        void WriteOutput(string output);

        void WriteSymb(string output);
        string ReadOutput();
        void Clear();

        void SetCursorPosition(int x, int y);
    }
}