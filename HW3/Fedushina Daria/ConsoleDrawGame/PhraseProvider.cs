using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleDrawGame
{
    class PhraseProvider : IPhraseProvider
    {
        private readonly GameSettings gameSettings;
        private Dictionary<string, string> FileData;
        public PhraseProvider(ISettingsProvider settingsProvider)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
            var language = gameSettings.Language;
            FileInfo jsonFile = new FileInfo($"{language}.json");
            if (!jsonFile.Exists)
            {
                throw new ArgumentException(
                   $"Can't find language file in {jsonFile}");
            }

            var jsonFileContent = File.ReadAllText(jsonFile.FullName);

            try
            {
                var jsonFileData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonFileContent);
                FileData = jsonFileData;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't extract file {language}.json", ex);
            }
        }
        public string GetPhrase(string phrase)
        {
            return FileData[phrase];
        }
    }
}
