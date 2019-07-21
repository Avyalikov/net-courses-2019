using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DoorsAndLevelsGame
{
    class JsonPhraseProvider : IPhraseProvider
    {
        private Dictionary<string, string> pharases;
        public string GetPhrase(string key)
        {
            if (!pharases.ContainsKey(key))
            {
                throw new Exception($"Phrase {key} is not found");
            }
            string phrase = pharases[key];
            return pharases[key];
        }

        public void SetLanguage(string language)
        {
            string languageFile = language+".json";
            using (StreamReader languageReader = new StreamReader("Resources\\Localization\\" + languageFile))
            {
                string rawFile = languageReader.ReadToEnd();
                pharases = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawFile);
                if (pharases == null)
                {
                    throw new Exception($"Language file {languageFile} is not correct");
                }
            }
        }
    }
}
