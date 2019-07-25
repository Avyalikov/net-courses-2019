using Newtonsoft.Json;
using System;
using System.IO;

namespace HW2_doors_and_levels_refactoring
{
    public class SettingsProvider : ISettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            var gameSettingsFile = new FileInfo("gamesettings.json");

            if (!gameSettingsFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find gamesettings json file. Trying to find it here: {gameSettingsFile.FullName}");
            }

            var textContent = File.ReadAllText(gameSettingsFile.FullName);

            try
            {
               return JsonConvert.DeserializeObject<GameSettings>(textContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read game settings content", ex);
            }
        }
    }
}
