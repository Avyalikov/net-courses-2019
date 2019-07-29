using Newtonsoft.Json;
using System;
using System.IO;

namespace doors_and_levels_game_after_refactoring
{
    class SettingsProvider: ISettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            var GameSettingsFile = new FileInfo("gamesettings.json");

            if (!GameSettingsFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find settings file {GameSettingsFile.FullName}");
            }
            var textContent = File.ReadAllText(GameSettingsFile.FullName);
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
