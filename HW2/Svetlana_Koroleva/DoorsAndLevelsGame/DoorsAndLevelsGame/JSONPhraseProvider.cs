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
       public string GetPhrase(string keyword)
        {
            var resourceFile = new FileInfo("Resources\\PhrasesEngLang.json");

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
