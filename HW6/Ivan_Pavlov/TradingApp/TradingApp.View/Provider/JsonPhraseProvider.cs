namespace TradingApp.View.Provider
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using TradingApp.View.Interface;

    class JsonPhraseProvider : IPhraseProvider
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
                    string.Format(
                        "Can't extract phrase value {0}. It is not in the dictionary. {1}",
                        phraseKey, e.Message));
            }
        }

        public void SetLanguage(string lang)
        {
            this.langPath = string.Format("Resource\\Json\\Lang{0}.json", lang);
        }

        private string GetData(string phraseKey)
        {
            SetLanguage("Ru");
            var resourceFile = new FileInfo(this.langPath);

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    string.Format(
                        "Can't find language file Lang{0}.json. Trying to find it here: {1}",
                        this.langPath, resourceFile));
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
                    string.Format(
                        "Can't extract phrase value {0}. {1}", phraseKey, e.Message));
            }
        }
    }
}
