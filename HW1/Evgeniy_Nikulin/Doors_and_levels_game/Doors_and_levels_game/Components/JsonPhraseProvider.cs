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
        public JsonPhraseProvider()
        {
            var resourceFile = new FileInfo("Resources/enLanguage.json");

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);
            resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
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