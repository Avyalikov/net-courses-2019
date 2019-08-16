// <copyright file="IPhraseProvider.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace Trading.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Associates a key phrase with sentences in the source (File, DB, ...)
    /// </summary>
    interface IPhraseProvider
    {
        /// <summary>
        /// Return text from source by phraseKey
        /// </summary>
        /// <param name="phraseKey">Key to phrase</param>
        /// <returns>Phrase string</returns>
        string GetPhrase(string phraseKey);

        /// <summary>
        /// Get phrase and replace part to settings property
        /// </summary>
        /// <param name="phraseKey">Key to phrase</param>
        /// <param name="rewriteStr">Substring for replace</param>
        /// <param name="rightStr">Right substring - settings property</param>
        /// <returns>Phrase string</returns>
        string GetPhraseAndReplace(string phraseKey, string rewriteStr, string rightStr);

        ///// <summary>
        ///// Get phrase, settings and replace
        ///// </summary>
        ///// <param name="phraseKey">Key to phrase</param>
        ///// <param name="settings">Settings of system</param>
        ///// <param name="settingsNames">Settings names for replacing into phrase</param>
        ///// <returns>Phrase string</returns>
        //string GetPhraseAndSettingsAndReplace(string phraseKey, Dictionary<string, string> settings, params string[] settingsNames);

        /// <summary>
        /// Set language source
        /// </summary>
        /// <param name="lang">Language key</param>
        void SetLanguage(string lang);
    }
}
