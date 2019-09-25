// <copyright file="JsonSettingsProvider.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace Trading.DataModel
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
        public Settings GetSettings()
        {
            var gameSettingsFile = new FileInfo("Settings.json");

            if (!gameSettingsFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find gamesettings json file. Trying to find it here: {gameSettingsFile.FullName}");
            }

            var textContent = File.ReadAllText(gameSettingsFile.FullName);

            try
            {
                return JsonConvert.DeserializeObject<Settings>(textContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read game settings content", ex);
            }
        }
    }
}
