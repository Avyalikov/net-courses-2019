namespace DrawGame
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using DrawGame.Interfaces;
    using Newtonsoft.Json;

    public class JsonPhraseProvider : IPhraseProvider
    {
        public string GetPhrase(string langPackName, KeysForPhrases phraseKey)
        {
            var resourceFile = new FileInfo($"LangPacks/{langPackName}.json");

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file. Trying to find it here: {resourceFile}");
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);

            try
            {
                var resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
                return resourceData[phraseKey.ToString()];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey.ToString()}", ex);
            }
        }
    }
}