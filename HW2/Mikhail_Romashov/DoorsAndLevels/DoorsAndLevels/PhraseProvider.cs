using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace DoorsAndLevels
{
    public enum Languages
    {
        Eng,
        Rus
    }
    public class PhraseProvider : IPhraseProvider
    {
        private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        public PhraseProvider(Languages language)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string langFileName = "Resources/EngLang.xml"; //default language file name

            if (language == Languages.Rus) 
                langFileName = "Resources/RusLang.xml";

            var resourceFile = new FileInfo(langFileName);

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
            return keyValuePairs[phraseKey];
        }
    }
}
