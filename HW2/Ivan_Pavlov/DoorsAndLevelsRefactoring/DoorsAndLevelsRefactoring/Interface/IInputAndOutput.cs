namespace DoorsAndLevelsRefactoring.Interface
{
    interface IInputAndOutput
    {
        string ReadInput();

        void WriteDoors(string Doors);

        char ReadKeyForExit();
    }
}
