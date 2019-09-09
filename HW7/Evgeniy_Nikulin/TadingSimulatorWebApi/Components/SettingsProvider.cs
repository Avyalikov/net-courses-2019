namespace TradingSimulatorWebApi.Components
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using TradingSimulator.Core;
    using TradingSimulator.Core.Interfaces;

    public class SettingsProvider : ISettingsProvider
    {
        public GameSettings Get()
        {
            var gameSettingsFile = new FileInfo("Resources/GameSettings.json");
            if (!gameSettingsFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find gamesettings json file. Trying to find it here: {gameSettingsFile.FullName}");
            }

            var resourceFileContent = File.ReadAllText(gameSettingsFile.FullName);
            try
            {
                return JsonConvert.DeserializeObject<GameSettings>(resourceFileContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read game settings content", ex);
            }
        }
    }
}