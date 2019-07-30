// <copyright file="JsonSettingsProvider.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// Basic game logic class
    /// </summary>
    internal class JsonSettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// The GetGameSettings
        /// </summary>
        /// <returns>The <see cref="GameSettings"/></returns>
        public GameSettings GetGameSettings()
        {
            var gameSettingsFile = new FileInfo("GameSettings.json");

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
