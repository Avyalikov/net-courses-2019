//-----------------------------------------------------------------------
// <copyright file="IPhraseProvider.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------   
namespace DashboardGame
{
    /// <summary>
    /// This interface is used for providing a game with phrases.
    /// </summary>
    internal interface IPhraseProvider
    {
        /// <summary>
        /// This method should return a string with phrase for given type.
        /// </summary>
        /// <param name="type">Type of phrase.</param>
        /// <returns>String with phrase.</returns>
        string GetPhrase(PhraseTypes type);
    }
}
