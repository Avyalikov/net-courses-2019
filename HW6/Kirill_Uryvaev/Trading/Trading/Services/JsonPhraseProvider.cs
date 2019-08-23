using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;

namespace Trading.ConsoleApp
{
    class JsonPhraseProvider : IPhraseProvider
    {
        private Dictionary<string, string> phrases;
        private string language = "English";
        public JsonPhraseProvider()
        {
            string languageFile = language + ".json";
            using (StreamReader languageReader = new StreamReader("Resources\\Localization\\" + languageFile))
            {
                string rawFile = languageReader.ReadToEnd();
                phrases = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawFile);
                if (phrases == null)
                {
                    throw new Exception($"Language file {languageFile} is not correct");
                }
            }
        }
        public string GetPhrase(string phrase)
        {
            if (!phrases.ContainsKey(phrase))
            {
                throw new ArgumentException($"Not found such prase {phrase}");
            }
            return phrases[phrase];
        }
    }
}
