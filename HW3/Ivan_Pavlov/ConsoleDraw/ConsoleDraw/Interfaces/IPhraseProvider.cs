// <copyright file="IPhraseProvider.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw.Interfaces
{
    /// <summary>
    /// phrase interface.
    /// </summary>
    public interface IPhraseProvider
    {
        /// <summary>
        /// Get phrase.
        /// </summary>
        /// <param name="phraseKey"> string key phrase.</param>
        /// <returns>string from json.</returns>
        string GetPhrase(string phraseKey);

        /// <summary>
        /// change lang.
        /// </summary>
        /// <param name="lang"> string lang.</param>
        void SetLanguage(string lang);
    }
}
