namespace GameWhichCanDraw.Components
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    internal class JsonSettingsProvider : Interfaces.ISettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            // var gameSettingsFile = new FileInfo("..\\..\\GameSettings.json"); // test local path
            var gameSettingsFile = new FileInfo("GameSettings.json");

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
