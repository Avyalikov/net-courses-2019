namespace ConsoleDrawGame.Classes
{
    using System;
    using System.IO;
    using ConsoleDrawGame.Interfaces;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class JsonPhraseProvider : IPhraseProvider
    {
        private readonly GameSettings gameSettings;
        private readonly string selectedLanguage;

        private Dictionary<string, string> resourceData = new Dictionary<string, string>();
        private Dictionary<string, string> jsonFilePath = new Dictionary<string, string>
        {
            { "english", "Resources/engLanguage.json" }
        };

        public JsonPhraseProvider(ISettingsProvider settingsProvider)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
            this.selectedLanguage = this.gameSettings.Language.ToLower().Trim();
        }

        /// <summary>Returns phrase by key</summary>
        /// <param name="phraseKey">Key for phrase.</param>
        /// <returns></returns>
        public string GetPhrase(string phraseKey)
        {
            if (this.resourceData.Count == 0)
            {
                this.GetData();
            }

            try
            {
                return this.resourceData[phraseKey];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey}", ex);
            }
        }

        /// <summary>Loads data from json file.</summary>
        private void GetData()
        {
            var resourceFile = new FileInfo(this.jsonFilePath[this.selectedLanguage]);
            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file {jsonFilePath[selectedLanguage]}. " +
                    $"Note: only english is supported now." +
                    $" Trying to find it here: {resourceFile}");
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);
            this.resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
        }
    }
}
