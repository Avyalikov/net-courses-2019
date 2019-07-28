//-----------------------------------------------------------------------
// <copyright file="SettingsProvider.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Components
{
    using System;
    using System.IO;
    using Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// Settings provider class
    /// </summary>
    public class SettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// Get the settings
        /// </summary>
        /// <returns>Return <see cref="GameSettings" /> class</returns>
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