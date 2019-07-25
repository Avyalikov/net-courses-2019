using Newtonsoft.Json;
using System;
using System.IO;

namespace Doors_and_levels_game_after_refactoring
{
        public class SettingsProvider : ISettingsProvider
        {
            public GameSettings GetGameSettings()
            {
                var gameSettingsFile = new FileInfo("..\\..\\GameSettings.json");

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
