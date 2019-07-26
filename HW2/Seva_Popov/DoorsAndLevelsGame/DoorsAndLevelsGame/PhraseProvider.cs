using DoorsAndLevelsGame.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DoorsAndLevelsGame
{
    class PhraseProvider : IPhraseProvider
    {
        public string GetPhrase(string phraseKey)
        {
            string Language;
            ISettingsProvider settingsProvider = new SettingsProvider();
            Language = settingsProvider.GetGameSettings().Language;
            //You can change language in GameSettings: "Resources\\eng.json";

            var resourceFile = new FileInfo(Language);

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                     $"Can't find language file rus.json. Trying to find it here: {resourceFile}");
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);

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
