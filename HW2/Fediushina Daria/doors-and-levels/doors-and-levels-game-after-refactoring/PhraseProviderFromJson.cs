using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace doors_and_levels_game_after_refactoring
{
    class PhraseProviderFromJson : IPhraseProvider
    {
        private readonly GameSettings gameSettings;
        private Dictionary <string, string> FileData;
        public PhraseProviderFromJson(ISettingsProvider settingsProvider)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
            var language = gameSettings.Language;
            var jsonFile = new FileInfo($"Resources/{language}.json"); 
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
        public string getPhrase(string phrase)
        {
            return FileData[phrase];
        }
    }
}
