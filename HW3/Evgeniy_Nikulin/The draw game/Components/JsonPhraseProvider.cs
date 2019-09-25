//-----------------------------------------------------------------------
// <copyright file="JsonPhraseProvider.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// Phrase provider class
    /// </summary>
    public class JsonPhraseProvider : IPhraseProvider
    {
        /// <summary>
        /// Resource data dictionary
        /// </summary>
        private Dictionary<string, string> resourceData = new Dictionary<string, string>();

        /// <summary>
        /// Initialization method
        /// </summary>
        /// <param name="lng">Language setting</param>
        public void Init(string lng)
        {
            var resourceFile = new FileInfo($"Resources/{lng}Language.json");
            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language json file. Trying to find it here: {resourceFile.FullName}");
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);
            try
            {
                this.resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read language file content", ex);
            }
        }

        /// <summary>
        /// Get the phrase
        /// </summary>
        /// <param name="phrase">Chosen phrase</param>
        /// <returns>Return the phrase</returns>
        public string GetPhrase(Phrase phrase)
        {
            try
            {
                return this.resourceData[phrase.ToString()];
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}