using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DoorsAndLevelsGame
{
    public class JSONPhraseProvider : IPhraseProvider
    {

        private Dictionary<string, string> SetFilePaths()
        {
            Dictionary<string, string> fileNames = new Dictionary<string, string>();
            fileNames.Add("eng", "Resources\\PhrasesEngLang.json");
            fileNames.Add("ru", "Resources\\PhrasesRuLang.json");
            return fileNames;
        }

        private string language = "eng";

        public void SetLanguage(string language)
        {
            this.language = language;
        }

        public string GetPhrase(string keyword)
        {
            Dictionary<string, string> fileNames = this.SetFilePaths();
            var resourceFile = new FileInfo(fileNames[this.language]);

            if (!resourceFile.Exists)
            {
                throw new ArgumentException($"The language file {resourceFile.Name} doesn't exist");
            }
            var resourceFileContent = File.ReadAllText(resourceFile.FullName);
            try
            {
                var resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);

                return resourceData[keyword];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"There is no element under {keyword}", ex);
            }
        }
    }
}
