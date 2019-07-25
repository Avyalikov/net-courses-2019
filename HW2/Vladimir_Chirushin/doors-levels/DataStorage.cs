using System;
using System.Collections.Generic;
using System.Linq;

namespace doors_levels
{
    public class DataStorage : IDataStorage
    {
        private Stack<Int32> levelsStack = new Stack<Int32>();

        public Int32 GetLastDoor()
        {
            return levelsStack.Pop();
        }

        public Boolean IsEmpty()
        {
            return levelsStack.Count == 0;
        }

        public void Clear()
        {
            levelsStack.Clear();
        }
        public void PushLastDoor(Int32 lastDoor)
        {
            levelsStack.Push(lastDoor);
        }
    }
}
