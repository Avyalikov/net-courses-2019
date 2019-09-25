using System;
using System.Collections.Generic;
using System.Xml;

namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This is an implementation of IPhraseProvider interface.
    /// </summary>
    class SimplePhraseProvider : IPhraseProvider
    {
        /// <summary>
        /// This dictionary contains all phrases which can be used in game. Key is an element of PhraseTypes enum, Value is a string with phrase.
        /// </summary>
        public readonly Dictionary<PhraseTypes, string> phrases;
        public SimplePhraseProvider(Languages lang)
        {
            phrases = ReadPhrasesFromFile(lang);
        }

        string IPhraseProvider.GetPhrase(PhraseTypes type)
        {
            return phrases[type];
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
    }
}
