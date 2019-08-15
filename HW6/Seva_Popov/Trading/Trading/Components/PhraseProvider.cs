using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Trading.Interfaces;

namespace Trading.Components
{
    class PhraseProvider : IPhraseProvider
    {
        public string GetPhrase(string phraseKey, Settings settings)
        {
            string Language = settings.Language;
            //ISettingsProvider settingsProvider = new SettingsProvider();

            //You can change language in GameSettings: "Resources\\Rng.json";

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
