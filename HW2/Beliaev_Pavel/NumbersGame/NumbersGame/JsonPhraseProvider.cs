using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using NumbersGame.Interfaces;

namespace NumbersGame
{
    public class JsonPhraseProvider : IPhraseProvider
    {
        public string GetPhrase(KeysForPhrases phraseKey, string langPackName)
        {
            var resourceFile = new FileInfo($"Resources/{langPackName}.json");

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