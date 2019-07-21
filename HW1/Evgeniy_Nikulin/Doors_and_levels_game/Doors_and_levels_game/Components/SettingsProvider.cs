using Doors_and_levels_game.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Doors_and_levels_game.Components
{
    public class SettingsProvider : ISettingsProvider
    {
        GameSettings gameSettings;
        public SettingsProvider()
        {
            var gameSettingsFile = new FileInfo("Resources/GameSettings.json");
            if (!gameSettingsFile.Exists)
                throw new ArgumentException(
                    $"Can't find gamesettings json file. Trying to find it here: {gameSettingsFile.FullName}");


            var resourceFileContent = File.ReadAllText(gameSettingsFile.FullName);
            try
            {
                gameSettings = JsonConvert.DeserializeObject<GameSettings>(resourceFileContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read game settings content", ex);
            }
        }
        public GameSettings Get() => gameSettings;
    }
}