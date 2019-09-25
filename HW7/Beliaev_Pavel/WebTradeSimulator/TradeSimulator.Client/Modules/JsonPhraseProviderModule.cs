using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Client.Interfaces;
using TradeSimulator.Client.Misc;

namespace TradeSimulator.Client.Modules
{
    internal class JsonPhraseProviderModule : IPhraseProvider
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
