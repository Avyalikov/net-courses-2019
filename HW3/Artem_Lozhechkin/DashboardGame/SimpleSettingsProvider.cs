//-----------------------------------------------------------------------
// <copyright file="SimpleSettingsProvider.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DashboardGame
{
    using System;
    using System.Xml;

    /// <summary>
    /// This class loads and stores the Settings instance.
    /// </summary>
    internal class SimpleSettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// Initializes a new instance of the SimpleSettingsProvider class.
        /// </summary>
        public SimpleSettingsProvider()
        {
            (Languages lang, int x, int y) = this.ReadConfigurationFile();
            this.SettingsData = new Settings(lang, x, y);
        }

        /// <summary>
        /// Gets or sets Settings instance.
        /// </summary>
        private Settings SettingsData { get; set; }

        /// <summary>
        /// This method returns Settings instance which is stored in SimpleSettingsProvider instance.
        /// </summary>
        /// <returns>Settings instance.</returns>
        public Settings GetSettings()
        {
            return this.SettingsData;
        }

        /// <summary>
        /// This method reads configuration data from file and returns it as a tuple. 
        /// </summary>
        /// <returns>Tuple which stores: Language, width of the board and height of the board.</returns>
        private (Languages, int, int) ReadConfigurationFile()
        {
            XmlDocument configFile = new XmlDocument();

            configFile.Load("Resources/config.xml");

            Languages lang = (Languages)Enum.Parse(typeof(Languages), configFile.SelectSingleNode("configuration/setting[@type='language']").InnerText);
            int x = int.Parse(configFile.SelectSingleNode("configuration/setting[@type='boardWidth']").InnerText);
            int y = int.Parse(configFile.SelectSingleNode("configuration/setting[@type='boardHeight']").InnerText);
            return (lang, x, y);
        }
    }
}
