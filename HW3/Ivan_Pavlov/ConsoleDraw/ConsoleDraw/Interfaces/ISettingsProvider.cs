// <copyright file="ISettingsProvider.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw.Interfaces
{
    /// <summary>
    /// settings interface.
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Gets game settings.
        /// </summary>
        /// <returns> return game seetting.</returns>
        GameSettings GetGameSettings();
    }
}
