using System;
using System.Xml;

namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This class implements ISettings provider and contains Settings for the game. 
    /// </summary>
    class SimpleSettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// This field contains Settings instance which stores game data.
        /// </summary>
        private Settings SettingsData { get; set; }
        /// <summary>
        /// This method creates a Settings instance.
        /// </summary>
        /// <param name="lang">Language for game.</param>
        /// <param name="numberOfDoors">Integer number of doors.</param>
        public SimpleSettingsProvider()
        {
            (Languages lang, int numberOfDoors) = ReadConfigurationFile();
            SettingsData = new Settings(lang, numberOfDoors);
        }
        private (Languages, int) ReadConfigurationFile()
        {
            XmlDocument configFile = new XmlDocument();

            configFile.Load("Resources/config.xml");

            Languages lang = (Languages)Enum.Parse(typeof(Languages), configFile.SelectSingleNode("configuration/setting[@type='language']").InnerText);
            int numberOfDoors = int.Parse(configFile.SelectSingleNode("configuration/setting[@type='numberOfDoors']").InnerText);

            return (lang, numberOfDoors);
        }
        /// <summary>
        /// This method returns Settings instance.
        /// </summary>
        /// <returns></returns>
        public Settings GetSettings()
        {
            return SettingsData;
        }
        /// <summary>
        /// This method returns a number of doors from Settings instance.
        /// </summary>
        /// <returns>Integer number of doors.</returns>
        int ISettingsProvider.GetNumberOfDoors()
        {
            return SettingsData.numberOfDoors;
        }
    }
}
