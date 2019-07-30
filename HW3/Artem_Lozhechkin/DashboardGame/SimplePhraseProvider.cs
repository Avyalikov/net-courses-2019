//-----------------------------------------------------------------------
// <copyright file="SimplePhraseProvider.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DashboardGame
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// This is an implementation of IPhraseProvider interface.
    /// </summary>
    internal class SimplePhraseProvider : IPhraseProvider
    {
        /// <summary>
        /// Initializes a new instance of the SimplePhraseProvider class.
        /// </summary>
        /// <param name="lang">Language for loading phrases.</param>
        public SimplePhraseProvider(Languages lang)
        {
            this.Phrases = this.ReadPhrasesFromFile(lang);
        }

        /// <summary>
        /// Gets a dictionary with a phrases. 
        /// </summary>
        public Dictionary<PhraseTypes, string> Phrases { get; }

        /// <summary>
        /// Returns a string with phrase according to the received PhraseType.
        /// </summary>
        /// <param name="type">Type of phrase.</param>
        /// <returns>String with phrase.</returns>
        string IPhraseProvider.GetPhrase(PhraseTypes type)
        {
            return this.Phrases[type];
        }

        /// <summary>
        /// This method reads phrases stored in Resources/strings.xml according to the chosen language, adds to the Dictionary and returns it for using that phrases in game.
        /// </summary>
        /// <param name="lang">Language of phrases.</param>
        /// <returns>Dictionary with phrases.</returns>
        private Dictionary<PhraseTypes, string> ReadPhrasesFromFile(Languages lang)
        {
            Dictionary<PhraseTypes, string> phrases = new Dictionary<PhraseTypes, string>();
            XmlDocument phrasesFile = new XmlDocument();

            phrasesFile.Load("Resources/strings.xml");

            foreach (XmlNode item in phrasesFile.SelectSingleNode($"languages/language[@name = \"{lang.ToString()}\"]").ChildNodes)
            {
                phrases.Add((PhraseTypes)Enum.Parse(typeof(PhraseTypes), item.Attributes["type"].Value), item.InnerText);
            }

            return phrases;
        }
    }
}
