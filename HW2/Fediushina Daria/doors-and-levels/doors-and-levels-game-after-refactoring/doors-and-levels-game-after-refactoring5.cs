using Newtonsoft.Json;
using System;
using System.IO;

namespace doors_and_levels_game_after_refactoring
{
    class SettingsProvider: ISettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            var GameSettingsProvider = new FileInfo("Resources/GameSettings.json");
            if (!GameSettingsProvider.Exists)
            {
                throw new ArgumentException(
                    $"Can't find settings file {GameSettingsProvider.FullName}");
            }
            var textContent = File.ReadAllText(GameSettingsProvider.FullName);
            try
            {
                return JsonConvert.DeserializeObject<GameSettings>(textContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read game settings content", ex);
            }0
        }
    }
}
