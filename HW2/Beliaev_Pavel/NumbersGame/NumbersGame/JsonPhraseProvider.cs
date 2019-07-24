using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using NumbersGame.Interfaces;

namespace NumbersGame
{
    public class JsonPhraseProvider : IPhraseProvider
    {
        public string GetPhrase(string phraseKey, Language ChosenLang)
        {
            string path = "";
            
            switch (ChosenLang)
            {
                case Language.Eng: { path = "Resources/English.json"; break; }
                case Language.Rus: { path = "Resources/Russian.json"; break; }                
            }
            var resourceFile = new FileInfo(path);

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file. Trying to find it here: {resourceFile}");
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