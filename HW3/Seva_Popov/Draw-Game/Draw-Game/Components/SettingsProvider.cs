namespace Draw_Game.Components
{
    using Draw_Game.Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class SettingsProvider : ISettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            var gameSettingsFile = new FileInfo("Resources\\GameSettings.json");

            if (!gameSettingsFile.Exists)
            {
                throw new ArgumentException($"Can't find gamesettings json file. Trying to find it here {gameSettingsFile.FullName}");
            }

            var textContent = File.ReadAllText(gameSettingsFile.FullName);

            try
            {
                return JsonConvert.DeserializeObject<GameSettings>(textContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Can't read game settings content", ex);
            }
        }
    }
}
