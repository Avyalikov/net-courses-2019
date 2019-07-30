using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame
{
    public enum Languages { English, Russian}
    public class GameSettings
    {
        private const int settingsNumber = 4;

        public Languages Language;
        public string ExitString;
        public int BoardWidth;
        public int BoardHeight;

        public GameSettings(List<string> settings)
        {
            if (settings.Count!= settingsNumber)
            {
                throw new Exception("Invalid settings number");
            }

            switch (settings[0])
            {
                case "English":
                    Language = Languages.English;
                    break;
                case "Russian":
                    Language = Languages.Russian;
                    break;
                default:
                    throw new Exception($"Language {settings[0]} is not supported");                    
            }

            if (settings[1]=="")
            {
                throw new Exception("Exit string cannot be empty");
            }
            ExitString = settings[1];

            if (!int.TryParse(settings[2], out BoardWidth))
            {
                throw new Exception("BoardWidth setting is not correct");
            }
            if (BoardWidth<10)
            {
                throw new Exception("BoardWidth must be greater than 1");
            }

            if (!int.TryParse(settings[3], out BoardHeight))
            {
                throw new Exception("VerticalChar setting is not correct");
            }
            if (BoardHeight < 10)
            {
                throw new Exception("BoardHeight must be greater than 1");
            }
        }
    }
}
