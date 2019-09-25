namespace DrawGame.Interfaces
{
    internal interface IInputOutput
    {
        string ReadInput();

        char ReadKey();

        void WriteOutput(string dataToOutput);
    }
}
