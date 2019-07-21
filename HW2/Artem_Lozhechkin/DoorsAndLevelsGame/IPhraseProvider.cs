using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This interface is used for providing a game with phrases.
    /// </summary>
    interface IPhraseProvider
    {
        /// <summary>
        /// This method should return a string with phrase for given type.
        /// </summary>
        /// <param name="type">Type of phrase.</param>
        /// <returns>String with phrase.</returns>
        string GetPhrase(PhraseTypes type);
    }
}
