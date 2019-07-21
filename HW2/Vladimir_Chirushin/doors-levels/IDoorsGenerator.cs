using System;

namespace doors_levels
{
    public interface IDoorsGenerator
    {
        Int32[] GetDoors(Int32 doorsAmount);
    }
}
