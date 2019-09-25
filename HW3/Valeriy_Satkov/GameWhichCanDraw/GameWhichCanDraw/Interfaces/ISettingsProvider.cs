// <copyright file="ISettingsProvider.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw.Interfaces
{
    /// <summary>
    /// Get Game Settings from source
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Get Game Settings from source
        /// </summary>
        /// <returns>GameSettings object with setting properties</returns>
        GameSettings GetGameSettings();
    }
}
