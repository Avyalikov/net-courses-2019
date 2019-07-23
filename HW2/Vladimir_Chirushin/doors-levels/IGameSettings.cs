using System;

namespace doors_levels
{
    public interface IGameSettings
    {
        Int32 GetMaxDoors();
        Int32 GetMaxDoorValue();
        Int32 GetMinDoorValue();
        Int32 GetBackNumber();
        String GetLanguagePath();
        void InitiateSettings();
    }
}
