namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This interface is used to identify some entity which can get and provide game settings.
    /// </summary>
    interface ISettingsProvider
    {
        /// <summary>
        /// Returns an instance of Settings class which contains Game settings.
        /// </summary>
        /// <returns>Instance of Settings class which contains Game settings.</returns>
        Settings GetSettings();
        /// <summary>
        /// Updates the current Settings.
        /// </summary>
        void UpdateSettings();
        /// <summary>
        /// Returns a phrase stored in Settings.
        /// </summary>
        /// <param name="type">Type for phrase.</param>
        /// <returns>A phrase string.</returns>
        string GetPhrase(PhraseTypes type);
        /// <summary>
        /// Returns number of doors which is stored in Settings.
        /// </summary>
        /// <returns>Integer number of doors.</returns>
        int GetNumberOfDoors();
    }
}
