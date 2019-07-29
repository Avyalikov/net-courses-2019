namespace DoorsAndLevelsRefactoring.Provider
{
    using DoorsAndLevelsRefactoring.Interface;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;

    class JsonPhraseProvider : IPhraseProvider
    {
        private readonly string resourceUrl;

        public JsonPhraseProvider(string resourceUrl)
        {
            this.resourceUrl = resourceUrl;
        }

        public string GetPhrase(string phraseKey)
        {          
            var resourceFile = new FileInfo(resourceUrl);

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    string.Format("Can't find this file {0}", resourceUrl));
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);

            try
            {
                var resourceData = JsonConvert.
                    DeserializeObject<Dictionary<string, string>>(resourceFileContent);
                return resourceData[phraseKey];
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    string.Format("Can't extract phrase value {0}", phraseKey), e.Message);
            }
        }
    }
}
