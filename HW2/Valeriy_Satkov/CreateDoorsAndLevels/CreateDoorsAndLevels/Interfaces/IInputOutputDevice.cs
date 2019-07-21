namespace CreateDoorsAndLevels.Interfaces
{
    /* Interaction between user and program
     */
    interface IInputOutputDevice
    {
        string ReadInput();
        char ReadKey();
        void WriteOutput(string dataToOutPut);
        void WriteSomeOutput(string dataToOutPut);
    }
}
