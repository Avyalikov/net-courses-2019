namespace NumbersGame.Interfaces
{
    public interface IInputOutput
    {
        string ReadInput();
        char ReadKey();
        void WriteOutput(string dataToOutput);
    }
}
