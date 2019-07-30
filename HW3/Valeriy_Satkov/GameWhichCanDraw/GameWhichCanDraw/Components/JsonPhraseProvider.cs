// <copyright file="JsonPhraseProvider.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw.Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;    
    using Newtonsoft.Json;
    
    /// <summary>
    /// Class - Associates a key phrase with sentences in the json-file
    /// </summary>
    internal class JsonPhraseProvider : Interfaces.IPhraseProvider
    {
        /// <summary>
        /// Path to language source
        /// </summary>
        private string langPath;

        /// <summary>
        /// Dictionary with all phrases from source
        /// </summary>
        private Dictionary<string, string> resourceData;

        /// <summary>
        /// Set language path
        /// </summary>
        /// <param name="lang">Key part of path to language source</param>
        public void SetLanguage(string lang)
        {
            this.langPath = $"Resources\\Lang{lang}.json";
        }

        /// <summary>
        /// Get phrase
        /// </summary>
        /// <param name="phraseKey">Key to phrase</param>
        /// <returns>Phrase string</returns>
        public string GetPhrase(string phraseKey)
        {
            if (this.resourceData == null)
            {
                return this.GetData(phraseKey);
            }

            try
            {
                return this.resourceData[phraseKey];
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey}. It is not in the dictionary.", e);
            }
        }

        /// <summary>
        /// Get phrase and replace part to GameSettings property
        /// </summary>
        /// <param name="phraseKey">Key to phrase</param>
        /// <param name="rewriteStr">Substring for replace</param>
        /// <param name="rightStr">Right substring - GameSettings property</param>
        /// <returns>Phrase string</returns>
        public string GetPhraseAndReplace(string phraseKey, string rewriteStr, string rightStr)
        {
            return this.GetPhrase(phraseKey).Replace(rewriteStr, rightStr);
        }

        /// <summary>
        /// Save all phrases to Dictionary from source and return phrase
        /// </summary>
        /// <param name="phraseKey">Key to phrase</param>
        /// <returns>Phrase string</returns>
        private string GetData(string phraseKey)
        {
            var resourceFile = new FileInfo(this.langPath);

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file LangRu.json. Trying to find it here: {resourceFile}");
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName); // string | get all lines

            try
            {
                this.resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent); // return Dictionary<string, string>
                return this.resourceData[phraseKey];
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    $"Can't extract phrase value {phraseKey}", e);
            }
        }
    }
}
