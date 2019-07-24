using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Doors_and_levels_game_after_refactoring
{
    public class JsonPhraseProvider : IPhraseProvider
    {
        private readonly string language;

        public JsonPhraseProvider(string m_language)
        {
            language = m_language;
        }
        public string GetPhrase(string phraseKey)
        {
            var resourceFile = new FileInfo($"..\\..\\Resources\\Lang{language}.json");


            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file LangRu.json. Trying to find it here: {resourceFile}");
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
