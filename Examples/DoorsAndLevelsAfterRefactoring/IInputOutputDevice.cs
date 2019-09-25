namespace DoorsAndLevelsAfterRefactoring
{
    public interface IInputOutputDevice
    {
        string ReadInput();
        char ReadKey();
        void WriteOutput(string dataToOutput);
    }
}
