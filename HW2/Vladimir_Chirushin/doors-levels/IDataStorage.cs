using System;

namespace doors_levels
{
    public interface IDataStorage
    {
        Int32 GetLastDoor();
        Boolean IsEmpty();
        void PushLastDoor(Int32 lastDoor);
        void Clear();
    }
}
