using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This class implements ISettings provider and contains Settings for the game. 
    /// </summary>
    class SimpleSettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// This field contains Settings instance which stores game data.
        /// </summary>
        private Settings SettingsData { get; set; }
        /// <summary>
        /// This method creates a Settings instance.
        /// </summary>
        /// <param name="lang">Language for game. Required for loading game phrases.</param>
        /// <param name="numberOfDoors">Integer number of doors.</param>
        public SimpleSettingsProvider(Languages lang, int numberOfDoors)
        {
            SettingsData = new Settings(lang, ReadPhrasesFromFile(lang), numberOfDoors);
        }
        /// <summary>
        /// This method reads phrases stored in Resources/strings.xml according to the chosen language and adds to the Dictionary for using that phrases in game.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This method returns Settings instance.
        /// </summary>
        /// <returns></returns>
        Settings ISettingsProvider.GetSettings()
        {
            return SettingsData;
        }
        /// <summary>
        /// This method updates phrases by replacing Settings instance.
        /// </summary>
        void ISettingsProvider.UpdateSettings()
        {
            SettingsData = new Settings(SettingsData.language, phrases:ReadPhrasesFromFile(SettingsData.language), SettingsData.numberOfDoors);
        }
        /// <summary>
        /// This method returns phrase from Settings according to the PhraseTypes argument.
        /// </summary>
        /// <param name="type">Type of the phrase. It is used as a key for phrases dictionary.</param>
        /// <returns></returns>
        string ISettingsProvider.GetPhrase(PhraseTypes type)
        {
            return SettingsData.phrases[type];
        }
        /// <summary>
        /// This method returns a number of doors from Settings instance.
        /// </summary>
        /// <returns>Integer number of doors.</returns>
        int ISettingsProvider.GetNumberOfDoors()
        {
            return SettingsData.numberOfDoors;
        }
    }
}
