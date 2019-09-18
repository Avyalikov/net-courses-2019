using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TradeSimulator.ConsoleApp.Interfaces;

namespace TradeSimulator.ConsoleApp
{
    public class JsonPhraseProvider : IPhraseProvider
    {
        public string GetPhrase(string langPackName, KeysForPhrases phraseKey)
        {
            var resourceFile = new FileInfo($"Resourses/{langPackName}.json");

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file. Trying to find it here: {resourceFile}");
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);

            try
            {
                var resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
                return resourceData[phraseKey.ToString()];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey.ToString()}", ex);
            }
        }
    }
}
