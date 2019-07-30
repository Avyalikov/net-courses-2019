// <copyright file="JsonSettingsProvider.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see cref="JsonSettingsProvider" />
    /// </summary>
    public class JsonSettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// Gets game settings from json file
        /// </summary>
        /// <returns>game settings</returns>
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
