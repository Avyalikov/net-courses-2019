namespace GameWhichCanDraw.Interfaces
{
    /* Interaction between user and program
     */
    public interface IInputOutputDevice
    {
        string ReadInput();

        void SetPosition(int x, int y);

        void WriteLineOutput(string dataToOutPut);

        void WriteOutput(string dataToOutPut);

        void Clear();
    }
}
