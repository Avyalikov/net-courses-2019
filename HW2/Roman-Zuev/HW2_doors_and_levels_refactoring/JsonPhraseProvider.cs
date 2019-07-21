using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HW2_doors_and_levels_refactoring
{
    public class JsonPhraseProvider : IPhraseProvider
    {
        public string GetPhrase(string phraseKey)
        {
            string LanguageChanger;
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            if (culture.ToString() == "ru-RU") LanguageChanger = "Resources\\Rus.json"; //choosing language
            else LanguageChanger = "Resources\\Eng.json";

            var resourceFile = new FileInfo(LanguageChanger);
            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file Eng.json. Trying to find it here: {resourceFile}");
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
