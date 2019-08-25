// <copyright file="JsonPhraseProvider.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace Trading.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see cref="JsonPhraseProvider" />
    /// </summary>
    internal class JsonPhraseProvider : IPhraseProvider
    {
        /// <summary>
        /// Defines the gameSettings
        /// </summary>
        private readonly Settings gameSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPhraseProvider"/> class.
        /// </summary>
        /// <param name="settingsProvider">The settingsProvider<see cref="ISettingsProvider"/></param>
        public JsonPhraseProvider(ISettingsProvider settingsProvider)
        {
            this.gameSettings = settingsProvider.GetSettings();
        }

        /// <summary>
        /// The GetPhrase
        /// </summary>
        /// <param name="phraseKey">The phraseKey<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string GetPhrase(string phraseKey)
        {
            string languageChanger;
            if (this.gameSettings.Language == "Rus")
            {
                languageChanger = "Resources\\Rus.json";
            }
            else
            {
                languageChanger = "Resources\\Eng.json";
            }

            var resourceFile = new FileInfo(languageChanger);
            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file {resourceFile.Name}. Trying to find it here: {resourceFile}");
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName, Encoding.UTF8);

            try
            {
                var resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
                return resourceData[phraseKey];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey}", ex);
            }
        }
    }
}
