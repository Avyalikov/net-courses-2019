using Newtonsoft.Json;
using System;
using System.IO;
using NumbersGame.Interfaces;

namespace NumbersGame
{
    public class SettingsProvider : ISettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            //Environment.GetFolderPath(Environment)
            //Path.;
            //var path = Environment.CurrentDirectory.;//"bin\\Debug");'\\'
            //Console.WriteLine(path);
            var gameSettingsFile = new FileInfo("gameSettings.json");
            
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
