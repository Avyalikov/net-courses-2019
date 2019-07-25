using System;

namespace doors_and_levels_game
{
    public class GameSettings
    {
        public int DoorsAmount { get; set; }
        public int UpperThresholdValue { get; set; }
        public int BackToPreviousLevelDoorNumber { get; set; }
        public string ExitCode { get; set; }
        public string Lang { get; set; }

    }
}
