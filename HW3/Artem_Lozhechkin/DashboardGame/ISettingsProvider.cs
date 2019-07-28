//-----------------------------------------------------------------------
// <copyright file="ISettingsProvider.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------   
namespace DashboardGame
{
    /// <summary>
    /// This interface is used to identify some entity which can get and provide game settings.
    /// </summary>
    internal interface ISettingsProvider
    {
        /// <summary>
        /// Returns an instance of Settings class which contains Game settings.
        /// </summary>
        /// <returns>Instance of Settings class which contains Game settings.</returns>
        Settings GetSettings();
    }
}
