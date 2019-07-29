using System;

namespace doors_levels
{
    public interface IGameSettings
    {
        Int32 GetMaxDoors();
        Int32 GetMaxDoorValue();
        Int32 GetMinDoorValue();
        String GetExitCommand();
        String GetLanguagePath();
        void InitiateSettings();
    }
}
