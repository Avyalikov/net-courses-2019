using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;


namespace GameWhichCanDraw
{
    public class JsonPhraseProvider : IPhraseProvider
    {
        private readonly string language;

        public JsonPhraseProvider(string language)
        {
            this.language = language;
        }
        public string GetPhrase(string phraseKey)
        {
            var resourceFile = new FileInfo($"..\\..\\Resources\\Lang{language}.json");


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
