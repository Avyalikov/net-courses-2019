//-----------------------------------------------------------------------
// <copyright file="PhraseProvider.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ConsoleDrawGame
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for work with language files (Resources folder)
    /// </summary>
    public class PhraseProvider : Interfaces.IPhraseProvider
    {
        /// <summary>
        /// Get phrase from language file
        /// </summary>
        /// <param name="phraseKey">phrase key from language file</param>
        /// <returns>string value from language file</returns>
        public string GetPhrase(string phraseKey)
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