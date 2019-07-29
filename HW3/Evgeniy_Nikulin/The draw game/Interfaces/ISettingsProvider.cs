//-----------------------------------------------------------------------
// <copyright file="ISettingsProvider.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Interfaces
{
    /// <summary>
    /// ISettingsProvider interface
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Get the settings
        /// </summary>
        /// <returns>Return <see cref="GameSettings" /> class</returns>
        GameSettings Get();
    }
}