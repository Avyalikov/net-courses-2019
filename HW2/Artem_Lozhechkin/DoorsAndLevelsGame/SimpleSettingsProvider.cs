using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DoorsAndLevelsGame
{
    class SimpleSettingsProvider : ISettingsProvider
    {
        private Settings SettingsData { get; set; }
        public SimpleSettingsProvider(Languages lang, int numberOfDoors)
        {
            SettingsData = new Settings(lang, ReadPhrasesFromFile(lang), numberOfDoors);
        }
        private Dictionary<PhraseTypes, string> ReadPhrasesFromFile(Languages lang)
        {
            Dictionary<PhraseTypes, string> phrases = new Dictionary<PhraseTypes, string>();
            XmlDocument phrasesFile = new XmlDocument();

            phrasesFile.Load("Resources/strings.xml");

            foreach (XmlNode item in phrasesFile.SelectSingleNode($"languages/language[@name = \"{lang.ToString()}\"]").ChildNodes)
            {
                phrases.Add((PhraseTypes)Enum.Parse(typeof(PhraseTypes), item.Attributes["type"].Value), item.Attributes["text"].Value);
            }

            return phrases;
        }
        Settings ISettingsProvider.GetSettings()
        {
            return SettingsData;
        }

        void ISettingsProvider.UpdateSettings()
        {
            SettingsData = new Settings(SettingsData.language, phrases:ReadPhrasesFromFile(SettingsData.language), SettingsData.numberOfDoors);
        }
        string ISettingsProvider.GetPhrase(PhraseTypes type)
        {
            return SettingsData.phrases[type];
        }

        int ISettingsProvider.GetNumberOfDoors()
        {
            return SettingsData.numberOfDoors;
        }
    }
}
