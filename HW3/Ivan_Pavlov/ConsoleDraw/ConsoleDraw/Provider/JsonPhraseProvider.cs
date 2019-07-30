// <copyright file="JsonPhraseProvider.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw.Provider
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using ConsoleDraw.Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// Phrase provider class.
    /// </summary>
    internal class JsonPhraseProvider : IPhraseProvider
    {
        private string langPath;

        private Dictionary<string, string> resourceData;

        /// <inheritdoc/>
        /// get tthe phrase
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
                    string.Format(
                        "Can't extract phrase value {0}. It is not in the dictionary. {1}", phraseKey, e.Message));
            }
        }

        /// <summary>
        /// set lang by setting.
        /// </summary>
        /// <param name="lang">set lang.</param>
        public void SetLanguage(string lang)
        {
            this.langPath = string.Format("Resources\\Lang{0}.json", lang);
        }

        private string GetData(string phraseKey)
        {
            var resourceFile = new FileInfo(this.langPath);

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    string.Format(
                        "Can't find language file Lang{0}.json. Trying to find it here: {1}", this.langPath, resourceFile));
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);

            try
            {
                this.resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
                return this.resourceData[phraseKey];
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    string.Format(
                        "Can't extract phrase value {0}. {1}", phraseKey, e.Message));
            }
        }
    }
}
