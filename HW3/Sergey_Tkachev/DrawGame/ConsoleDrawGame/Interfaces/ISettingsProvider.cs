namespace ConsoleDrawGame.Interfaces
{
    using ConsoleDrawGame.Classes;

    internal interface ISettingsProvider
    {
        /// <summary>Returns game settings from a file.</summary>
        /// <returns></returns>
        GameSettings GetGameSettings();
    }
}
