// <copyright file="IPhraseProvider.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    /// <summary>
    /// Phrase provider interface
    /// </summary>
    public interface IPhraseProvider
    {
        /// <summary>
        /// Get phrase method
        /// </summary>
        /// <param name="phraseKey">key phrase</param>
        /// <returns>All phrase</returns>
        string GetPhrase(string phraseKey);
    }
}
