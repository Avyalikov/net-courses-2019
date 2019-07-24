namespace DoorsAndLevelsRefactoring.Provider
{
    using DoorsAndLevelsRefactoring.Interface;
    using System;
    using System.Collections.Generic;

    class StackStorageProvider : IChooseDoorsStorage
    {
        private Stack<int> doorsStorage = new Stack<int>();

        public int Pop() 
        {
            if (doorsStorage.Count == 0)
                throw new InvalidOperationException(
                    "You can't lower the level");

            return doorsStorage.Pop();
        }

        public void Push(int Door)
        {
            doorsStorage.Push(Door);
        }
    }
}
