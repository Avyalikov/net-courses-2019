// <copyright file="ISettingsProvider.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    /// <summary>
    /// Settings provider interface
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Gets game settings
        /// </summary>
        /// <returns>game settings</returns>
        GameSettings GetGameSettings();
    }
}
