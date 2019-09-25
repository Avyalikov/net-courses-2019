//-----------------------------------------------------------------------
// <copyright file="SettingsProvider.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Components
{
    using System;
    using System.IO;
    using ConsoleDrawGame;
    using Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for work with setting file (Resources folder)
    /// </summary>
    internal class SettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// Include necessary option for game
        /// </summary>
        /// <returns>Class with option for game</returns>
        public GameSettings GameSettings()
        {
            var gameSettingsFile = new FileInfo("Resources/settings.json");

            if (!gameSettingsFile.Exists)
            {
                throw new ArgumentException($"Can't find gamesettings json file. Trying to find it here {gameSettingsFile.FullName}");
            }

            var textContent = File.ReadAllText(gameSettingsFile.FullName);

            try
            {
                return JsonConvert.DeserializeObject<GameSettings>(textContent);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Can't read gamesettings content", e);
            }
        }
    }
}