// <copyright file="IPhraseProvider.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw.Interfaces
{
    /// <summary>
    /// Associates a key phrase with sentences in the source (File, DB, ...)
    /// </summary>
    public interface IPhraseProvider
    {
        /// <summary>
        /// Return text from source by phraseKey
        /// </summary>
        /// <param name="phraseKey">Key to phrase</param>
        /// <returns>Phrase string</returns>
        string GetPhrase(string phraseKey);

        /// <summary>
        /// Get phrase and replace part of it
        /// </summary>
        /// <param name="phraseKey">Key to phrase</param>
        /// <param name="rewriteStr">Substring for replace</param>
        /// <param name="rightStr">Right substring</param>
        /// <returns>Phrase string</returns>
        string GetPhraseAndReplace(string phraseKey, string rewriteStr, string rightStr);

        /// <summary>
        /// Set language source
        /// </summary>
        /// <param name="lang">Language key</param>
        void SetLanguage(string lang);
    }
}
