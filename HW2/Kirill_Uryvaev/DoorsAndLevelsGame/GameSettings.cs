using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    public class GameSettings
    {
        private const int settingsNumber = 5;

        public string LanguageFile;
        public string ExitString;
        public int DoorsNumber;
        public int MaxDoorNumber;
        public int ExitDoorNumber;

        public GameSettings(List<string> settings)
        {
            if (settings.Count!= settingsNumber)
            {
                throw new Exception("Invalid settings number");
            }

            if (!File.Exists($"Resources\\Localization\\{settings[0]}.json"))
            {
                throw new Exception($"Language {settings[0]} is not supported");
            }
            LanguageFile = settings[0];

            if (settings[1]=="")
            {
                throw new Exception("Exit string cannot be empty");
            }
            ExitString = settings[1];

            if (!int.TryParse(settings[2], out DoorsNumber))
            {
                throw new Exception("DoorsNumber setting is not correct");
            }
            if (DoorsNumber < 2)
            {
                throw new Exception("Game must contain at least two doors");
            }

            if (!int.TryParse(settings[3], out MaxDoorNumber))
            {
                throw new Exception("MaxInitialDoorNumber setting is not correct");
            }
            if (MaxDoorNumber < 2)
            {
                throw new Exception("Maximum initial door number must be more than 1");
            }
            if (!int.TryParse(settings[4], out ExitDoorNumber))
            {
                throw new Exception("ExitDoorNumber setting is not correct");
            }
        }
    }
}
