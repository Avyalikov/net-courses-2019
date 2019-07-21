using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleDrawGame
{
    class FileSettingsProvider : ISettingsProvider
    {
        private string _settingsFile = "settings.json";
        public GameSettings GetSettings()
        {
            List<string> settingsStrings;
            using (StreamReader settingsReader = new StreamReader("Resources\\" + _settingsFile))
            {
                string rawFile = settingsReader.ReadToEnd();
                Dictionary<string,List<string>> settingsDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(rawFile);
                if (settingsDictionary == null)
                {
                    throw new Exception($"Settings file {_settingsFile} is not correct");
                }
                if (!settingsDictionary.ContainsKey("Settings"))
                {
                    throw new Exception($"Settings file {_settingsFile} is not correct");
                }
                settingsStrings = settingsDictionary["Settings"];
            }
            GameSettings gameSetting = new GameSettings(settingsStrings);
            return gameSetting;
        }
    }
}
