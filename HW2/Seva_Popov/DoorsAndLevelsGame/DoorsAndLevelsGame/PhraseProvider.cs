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
            
          Language = "Resources\\Rus.json"; 
          //Language = "Resources\\Eng.json";

            var resourceFile = new FileInfo(Language);

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
