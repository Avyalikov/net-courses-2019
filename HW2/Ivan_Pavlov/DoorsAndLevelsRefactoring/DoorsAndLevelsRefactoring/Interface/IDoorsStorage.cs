namespace DoorsAndLevelsRefactoring.Interface
{
    /// <summary>
    /// realize LIFO
    /// </summary>
    interface IDoorsStorage
    {
        void Push(int Door);

        int Pop();
    }
}
