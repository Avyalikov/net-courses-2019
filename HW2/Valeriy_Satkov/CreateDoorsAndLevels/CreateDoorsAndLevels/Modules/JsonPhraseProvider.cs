using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CreateDoorsAndLevels.Modules
{
    /* Associates a key phrase with sentences in the json-file
     */
    class JsonPhraseProvider : Interfaces.IPhraseProvider
    {
        private string langPath;

        public void SetLanguage(string lang)
        {
            this.langPath = $"Resources\\Lang{lang}.json";
        }

        public string GetPhrase(string phraseKey)
        {
            // var langPath = new FileInfo("..\\..\\Resources\\LangEn.json"); // test local path
            var resourceFile = new FileInfo(langPath);
            
            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file LangRu.json. Trying to find it here: {resourceFile}");
            }
            
            var resourceFileContent = File.ReadAllText(resourceFile.FullName); // string | get all lines

            try
            {
                var resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent); // return Dictionary<string, string>
                return resourceData[phraseKey];
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey}", e);
            }            
        }
    }
}
