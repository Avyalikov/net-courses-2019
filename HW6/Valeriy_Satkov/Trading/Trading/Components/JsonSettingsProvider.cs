// <copyright file="JsonSettingsProvider.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace Trading.Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// Class - Get Game Settings from JSON-file
    /// </summary>
    class JsonSettingsProvider : Interfaces.ISettingsProvider
    {
        /// <summary>
        /// Get Game Settings from JSON-file
        /// </summary>
        /// <returns>GameSettings object with setting properties</returns>
        public StockExchangeSettings GetSettings()
        {
            var settingsFile = new FileInfo("StockExchangeSettings.json");

            if (!settingsFile.Exists)
            {
                throw new ArgumentException($"Can't find gamesettings json file. Trying to find it here {settingsFile.FullName}");
            }

            var textContent = File.ReadAllText(settingsFile.FullName);            

            try
            {
                return JsonConvert.DeserializeObject<StockExchangeSettings>(textContent);
                // return JsonConvert.DeserializeObject<Dictionary<string, string>>(textContent);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Can't read settings content", e);
            }
        }
    }
}
