// <copyright file="IPhraseProvider.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    /// <summary>
    /// Defines the <see cref="IPhraseProvider" />
    /// </summary>
    internal interface IPhraseProvider
    {
        /// <summary>
        /// The GetPhrase
        /// </summary>
        /// <param name="phraseKey">The phraseKey<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        string GetPhrase(string phraseKey);
    }
}
