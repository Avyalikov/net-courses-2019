namespace DoorsAndLevelsRef
{
    public interface ISettingsProvider
    {
        /// <summary>Returns game settings from a file.</summary>
        /// <returns></returns>
        GameSettings GetGameSettings();
    }
}
