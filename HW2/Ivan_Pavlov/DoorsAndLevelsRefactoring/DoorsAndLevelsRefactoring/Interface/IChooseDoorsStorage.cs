namespace DoorsAndLevelsRefactoring.Interface
{   
    interface IChooseDoorsStorage
    {
        /// <summary>
        /// realize LIFO
        /// </summary>

        void Push(int Door);

        int Pop();

        bool HasValue();
    }
}
