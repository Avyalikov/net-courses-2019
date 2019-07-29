//-----------------------------------------------------------------------
// <copyright file="IPhraseProvider.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Interfaces
{
    /// <summary>
    /// Enum of phrases
    /// </summary>
    public enum Phrase
    {
        /// <summary>
        /// Welcome phrase
        /// </summary>
        Welcome,

        /// <summary>
        /// Your chosen figure phrase
        /// </summary>
        YourChosenFigure,

        /// <summary>
        /// Choose phrase
        /// </summary>
        Choose,

        /// <summary>
        /// Wrong value phrase
        /// </summary>
        WrongValue
    }

    /// <summary>
    /// Phrase provider interface
    /// </summary>
    public interface IPhraseProvider
    {
        /// <summary>
        /// Initialization method
        /// </summary>
        /// <param name="lng">Language setting</param>
        void Init(string lng);

        /// <summary>
        /// Get the phrase
        /// </summary>
        /// <param name="phrase">Chosen phrase</param>
        /// <returns>Return the phrase</returns>
        string GetPhrase(Phrase phrase);
    }
}