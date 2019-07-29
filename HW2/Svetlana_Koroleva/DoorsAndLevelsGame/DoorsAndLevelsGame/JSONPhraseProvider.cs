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

        private string language;

        public JSONPhraseProvider(string language)
        {
            this.language = language;
        }

        private Dictionary<string, string> phrases;
       
        public string GetPhrase(string keyword)
        {
           
             try
            {
                return phrases[keyword];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"There is no element under {keyword}", ex);
            }
        }

        public void ReadResourceFile()
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
                phrases = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);              
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read file {resourceFile.Name}", ex);
            }
        }
    }
}

