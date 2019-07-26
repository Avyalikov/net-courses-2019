using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DoorsAndLevelsRef
{
    internal class JsonPhraseProvider : IPhraseProvider
    {
        private readonly string selectedLanguage;
        private readonly GameSettings gameSettings;

        private Dictionary<string, string> resourceData = new Dictionary<string, string>();
        private Dictionary<string, string> jsonFilePath = new Dictionary<string, string>
        {
            {"english", "Resources/engLanguage.json"},
            {"russian", "Resources/rusLanguage.json"}
        };

        public JsonPhraseProvider(ISettingsProvider settingsProvider)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
            selectedLanguage = gameSettings.Language.ToLower().Trim();
        }

        /// <summary>Loads data from json file.</summary>
        private void GetData()
        {
            var resourceFile = new FileInfo(jsonFilePath[selectedLanguage]);
            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file LangRu.json. Trying to find it here: {resourceFile}");
            }
            var resourceFileContent = File.ReadAllText(resourceFile.FullName);
            resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);


        }

        /// <summary>Returns phare by key</summary>
        /// <param name="phraseKey">Key for phrase.</param>
        /// <returns></returns>
        public string GetPhrase(string phraseKey)
        {
            if (resourceData.Count == 0)
                GetData();
            try
            {
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