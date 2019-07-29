// <copyright file="JsonSettings.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw.Provider
{
    using System;
    using System.IO;
    using ConsoleDraw.Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// Settongs provider class.
    /// </summary>
    internal class JsonSettings : ISettingsProvider
    {
        /// <inheritdoc/>
        public GameSettings GetGameSettings()
        {
            var gameSetting = new FileInfo("Resources\\GameSettings.json");

            if (!gameSetting.Exists)
            {
                throw new ArgumentException(string.Format(
                    "Can't find gamesettings json file. Trying to find it here {0}", gameSetting.FullName));
            }

            var texttContent = File.ReadAllText(gameSetting.FullName);

            try
            {
                return JsonConvert.DeserializeObject<GameSettings>(texttContent);
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    string.Format(
                    "Can't read GameSetting content."), e.Message);
            }
        }
    }
}
