using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace doors_and_levels_game_after_refactoring
{
    class PhraseProviderFromJson : IPhraseProvider
    {
        public string getPhrase(string phrase)
        {
            var jsonFile = new FileInfo("Resources/LangEng.json");
            if (!jsonFile.Exists)
            {
                throw new ArgumentException(
                   $"Can't find language file in {jsonFile}");
            }

            var jsonFileContent = File.ReadAllText(jsonFile.FullName);

            try
            {
                var jsonFileData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonFileContent);
                return jsonFileData[phrase];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phrase}", ex);
            }
        }
    }
}
