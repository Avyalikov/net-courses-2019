using Doors_and_levels_game.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Doors_and_levels_game.Components
{
    public class JsonPhraseProvider : IPhraseProvider
    {
        Dictionary<string, string> resourceData;
        public JsonPhraseProvider(string lng)
        {
            var resourceFile = new FileInfo($"Resources/{lng}Language.json");
            if (!resourceFile.Exists)
                throw new ArgumentException(
                    $"Can't find language json file. Trying to find it here: {resourceFile.FullName}");

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);            
            try
            {
                resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read language file content", ex);
            }
        }

        public string GetPhrase(Phrase phrase)
        {
            try
            {
                return resourceData[phrase.ToString()];
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }
    }
}