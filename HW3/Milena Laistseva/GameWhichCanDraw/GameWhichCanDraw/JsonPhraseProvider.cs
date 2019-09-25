// <copyright file="JsonPhraseProvider.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see cref="JsonPhraseProvider" />
    /// </summary>
    public class JsonPhraseProvider : IPhraseProvider
    {
        /// <summary>
        /// Game language
        /// </summary>
        private readonly string language;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPhraseProvider"/> class.
        /// </summary>
        /// <param name="language">Game language</param>
        public JsonPhraseProvider(string language)
        {
            this.language = language;
        }

        /// <summary>
        /// Get phrase method
        /// </summary>
        /// <param name="phraseKey">Phrase key</param>
        /// <returns>All phrase</returns>
        public string GetPhrase(string phraseKey)
        {
            var resourceFile = new FileInfo($"..\\..\\Resources\\Lang{language}.json");

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file LangRu.json. Trying to find it here: {resourceFile}");
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
