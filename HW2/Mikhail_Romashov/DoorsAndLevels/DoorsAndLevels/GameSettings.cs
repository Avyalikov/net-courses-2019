using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    public class GameSettings
    {
        public int doorsAmount { get; set; }
        public int exitCode { get; set; }
        public int previousLevelCode { get; set; }
        public string gameLanguage { get; set; }
    }
}
