//-----------------------------------------------------------------------
// <copyright file="ISettingsProvider.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Interfaces
{
    using ConsoleDrawGame;

    /// <summary>
    /// Interface for work with setting file (Resources folder)
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Include necessary option for game
        /// </summary>
        /// <returns>Class with option for game</returns>
        GameSettings GameSettings();
    }
}
