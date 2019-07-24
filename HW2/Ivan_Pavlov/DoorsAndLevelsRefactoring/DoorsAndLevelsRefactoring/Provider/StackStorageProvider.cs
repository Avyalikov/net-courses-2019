namespace DoorsAndLevelsRefactoring.Provider
{
    using DoorsAndLevelsRefactoring.Interface;
    using System;
    using System.Collections.Generic;

    class StackStorageProvider : IChooseDoorsStorage
    {
        private Stack<int> Storage = new Stack<int>();

        public int Pop() 
        {
            return Storage.Pop();
        }

        public void Push(int Door)
        {
            Storage.Push(Door);
        }

        public bool HasValue()
        {
            if (Storage.Count > 0)
                return true;

            return false;
        }
    }
}
