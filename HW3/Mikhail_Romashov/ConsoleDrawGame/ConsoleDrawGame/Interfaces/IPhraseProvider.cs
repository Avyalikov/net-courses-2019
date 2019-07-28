//-----------------------------------------------------------------------
// <copyright file="IPhraseProvider.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Interfaces
{
    /// <summary>
    /// Interface for work with language files
    /// </summary>
    public interface IPhraseProvider
     {
        /// <summary>
        /// Get phrase from language file
        /// </summary>
        /// <param name="phraseKey">phrase key from language file</param>
        /// <returns>string value from language file</returns>
        string GetPhrase(string phraseKey);
     }
}
