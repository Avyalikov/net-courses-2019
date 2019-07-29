namespace ConsoleDraw.Interfaces
{
    public interface IInputOutputDevice
    {
        string ReadInput();

        void SetPosition(int x, int y);

        void WriteLineOutput(string dataToOutPut);

        void WriteOutput(string dataToOutPut);

        public void SetCursorPosition(int x, int y)

        void Clear();
    }
}
