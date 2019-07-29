namespace ConsoleDraw.Provider
{
    using ConsoleDraw.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    


    public class JsonPhraseProvider : IPhraseProvider
    {
        private string langPath;

        private Dictionary<string, string> resourceData;

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
                    string.Format("Can't extract phrase value {0}. It is not in the dictionary. {1}",
                    phraseKey, e.Message));
            }
        }

        private string GetData(string phraseKey)
        {
            var resourceFile = new FileInfo(this.langPath);

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    string.Format("Can't find language file LangRu.json. Trying to find it here: {0}", 
                    resourceFile));
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName); 

            try
            {
                this.resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent); 
                return this.resourceData[phraseKey];
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    string.Format("Can't extract phrase value {0}. {1}", 
                    phraseKey, e.Message));
            }
        }   

        public string GetPhraseAndReplace(string phraseKey, string rewriteStr, string rightStr)
        {
            throw new System.NotImplementedException();
        }

        public void SetLanguage(string lang)
        {
            this.langPath = string.Format("Resource\\Lang{0}.json", lang);
        }
    }
}
