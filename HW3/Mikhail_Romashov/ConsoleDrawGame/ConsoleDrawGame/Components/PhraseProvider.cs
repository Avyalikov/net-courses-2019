//-----------------------------------------------------------------------
// <copyright file="PhraseProvider.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for work with language files (Resources folder)
    /// </summary>
    public class PhraseProvider : IPhraseProvider
    {
        /// <summary>
        /// Storage with pairs key phrase
        /// </summary>
        private Dictionary<string, string> phrases;

        /// <summary>
        /// Get phrase from dictionary
        /// </summary>
        /// <param name="phraseKey">Phrase key</param>
        /// <returns>Phrase from dictionary</returns>
        public string GetPhrase(string phraseKey)
        {
            if (this.phrases == null)
            {
                return this.GetDictPharses(phraseKey);
            }

            try
            {
                return this.phrases[phraseKey];
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey}. It is not in the dictionary.", e);
            }
        }

        /// <summary>
        /// Get phrases from language file and initialize dictionary
        /// </summary>
        /// <param name="phraseKey">Phrase key</param>
        /// <returns>Phrase with key phraseKey</returns>
        public string GetDictPharses(string phraseKey)
        {
            var resourceFile = new FileInfo("Resources/langEng.json");

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file LangEng.json. Trying to find it here: {resourceFile}");
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);

            try
            {
                this.phrases = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
                return this.phrases[phraseKey];
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey}", ex);
            }
        }
    }
}