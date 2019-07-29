using ConsoleDrawGame.Classes;

namespace ConsoleDrawGame.Interfaces
{
    interface ISettingsProvider
    {
        /// <summary>Returns game settings from a file.</summary>
        /// <returns></returns>
        GameSettings GetGameSettings();
    }
}
