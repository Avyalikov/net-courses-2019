using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.ConsoleApp.Interfaces;

namespace TradeSimulator.ConsoleApp
{
    public class SettingsProvider : ISettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            var gameSettingsFile = new FileInfo("Resourses/gameSettings.json");

            if (!gameSettingsFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find gamesettings json file with this path: {gameSettingsFile.FullName}");
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
