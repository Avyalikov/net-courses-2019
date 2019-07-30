// <copyright file="ISettingsProvider.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    /// <summary>
    /// Defines the <see cref="ISettingsProvider" />
    /// </summary>
    internal interface ISettingsProvider
    {
        /// <summary>
        /// The GetGameSettings
        /// </summary>
        /// <returns>The <see cref="GameSettings"/></returns>
        GameSettings GetGameSettings();
    }
}
