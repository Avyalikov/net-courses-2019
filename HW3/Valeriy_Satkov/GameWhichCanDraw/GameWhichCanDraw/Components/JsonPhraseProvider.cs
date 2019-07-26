namespace GameWhichCanDraw.Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;    
    using Newtonsoft.Json;

    /* Associates a key phrase with sentences in the json-file
     */
    internal class JsonPhraseProvider : Interfaces.IPhraseProvider
    {
        private string langPath;

        private Dictionary<string, string> resourceData;

        public void SetLanguage(string lang)
        {
            this.langPath = $"Resources\\Lang{lang}.json";
        }        

        public string GetPhrase(string phraseKey)
        {
            if (this.resourceData == null)
            {
                return this.GetData(phraseKey);
            }

            try
            {
                return this.resourceData[phraseKey];
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey}. It is not in the dictionary.", e);
            }
        }

        private string GetData(string phraseKey)
        {
            var resourceFile = new FileInfo(this.langPath);

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file LangRu.json. Trying to find it here: {resourceFile}");
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName); // string | get all lines

            try
            {
                this.resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent); // return Dictionary<string, string>
                return this.resourceData[phraseKey];
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey}", e);
            }
        }
    }
}
