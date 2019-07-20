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
        public string GetPhrase(string phraseKey)
        {
            string filePath = "..\\..\\Resources\\LangEn.json";            
            var resourceFile = new FileInfo(filePath);
            
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
