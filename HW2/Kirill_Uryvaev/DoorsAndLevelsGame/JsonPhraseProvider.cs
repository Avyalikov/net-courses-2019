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
        private string languageFile = "textEn.json";
        public string GetPhrase(string key)
        {
            return pharases[key];
        }

        public JsonPhraseProvider()
        {
            using (StreamReader languageReader = new StreamReader("Resources\\" + languageFile))
            {
                string rawFile = languageReader.ReadToEnd();
                pharases = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawFile);
                if (pharases==null)
                {
                    throw new Exception($"Language file {languageFile} is not correct");
                }
            }
        }
    }
}
