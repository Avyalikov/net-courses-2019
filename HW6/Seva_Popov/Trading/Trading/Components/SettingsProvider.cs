using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Trading.Interfaces;

namespace Trading
{
    class SettingsProvider : ISettingsProvider
    {
        public Settings GetSettings()
        {
            var gameSettingsFile = new FileInfo("Resources\\Settings.json");

            if (!gameSettingsFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find gamesettings json file. Trying to find it here: {gameSettingsFile.FullName}");
            }

            var textContent = File.ReadAllText(gameSettingsFile.FullName);

            try
            {
                return JsonConvert.DeserializeObject<Settings>(textContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read game settings content", ex);
            }
        }
    }
}
