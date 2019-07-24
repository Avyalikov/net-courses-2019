using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doors_and_levels_game_after_refactoring
{
    public class GameSettings
    {
        public int DoorsAmount { get; set; }
        public int MaxLevel { get; set; }
        public int ExitDoorNumber { get; set; }

        public string ExitCode { get; set; }
        public string Language { get; set; }
    }
}
