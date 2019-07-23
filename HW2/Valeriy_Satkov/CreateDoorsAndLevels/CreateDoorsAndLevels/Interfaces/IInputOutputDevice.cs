namespace CreateDoorsAndLevels.Interfaces
{
    /* Interaction between user and program
     */
    interface IInputOutputDevice
    {
        string ReadInput();
        char ReadKey();
        void WriteLineOutput(string dataToOutPut);
        void WriteOutput(string dataToOutPut);
    }
}
