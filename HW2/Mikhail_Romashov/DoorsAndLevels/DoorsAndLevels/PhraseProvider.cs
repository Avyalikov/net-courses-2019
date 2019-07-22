using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace DoorsAndLevels
{
    public class PhraseProvider : IPhraseProvider
    {
        private Dictionary<string, string> keyValuePairs;
        private Dictionary<string, string> languagesFiles = new Dictionary<string, string>()
        {
            { "Rus", "Resources/RusLang.xml" },
            {  "Eng", "Resources/EngLang.xml" }
        };
        private GameSettings setting;
        public PhraseProvider()
        {
            ISettingsProvider settingsProvider = new SettingsProvider();
            setting = new GameSettings();
            setting = settingsProvider.gameSettings();
            keyValuePairs = new Dictionary<string, string>();

            XmlDocument xmlDoc = new XmlDocument();

            var resourceFile = new FileInfo(languagesFiles[setting.gameLanguage]);

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find file EngLang.xml. Trying to find it here: {resourceFile.FullName}");
            }

            xmlDoc.Load(resourceFile.FullName);
            XmlElement xmlRoot = xmlDoc.DocumentElement;

            foreach (XmlElement childNode in xmlRoot)
            { //add all the strings to Dictionary
                keyValuePairs.Add(childNode.Name, childNode.InnerText);
            }
        }
        public string GetPhrase(string phraseKey)
        {
            try
            {
                return keyValuePairs[phraseKey];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"The string with the key = '{phraseKey}' is not contained in '{languagesFiles[setting.gameLanguage]}'");
            }

        }
    }
}
