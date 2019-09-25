namespace DoorsAndLevelsRefactoring.Interface
{
    interface IInputAndOutput
    {
        string ReadInput();

        void WriteOutput(string Doors);

        char ReadKeyForExit();
    }
}
