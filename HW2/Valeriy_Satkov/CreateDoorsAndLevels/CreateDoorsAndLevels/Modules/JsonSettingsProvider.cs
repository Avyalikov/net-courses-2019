using Newtonsoft.Json;
using System;
using System.IO;

namespace CreateDoorsAndLevels.Modules
{
    class JsonSettingsProvider : Interfaces.ISettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            // string filePath = "..\\..\\LangEn.json"; // test local path
            string filePath = "GameSettings.json";
            var gameSettingsFile = new FileInfo(filePath);

            if (!gameSettingsFile.Exists)
            {
                throw new ArgumentException($"Can't find gamesettings json file. Trying to find it here {gameSettingsFile.FullName}");
            }

            var textContent = File.ReadAllText(gameSettingsFile.FullName);

            try
            {
                return JsonConvert.DeserializeObject<GameSettings>(textContent);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Can't read gamesettings content", e);
            }
        }
    }
}
