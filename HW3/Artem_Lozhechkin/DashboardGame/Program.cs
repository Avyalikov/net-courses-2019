//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------   
namespace DashboardGame
{
    /// <summary>
    /// This class contains an entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method is an entry point.
        /// </summary>
        /// <param name="args">Arguments for entry point.</param>
        private static void Main(string[] args)
        {
            ISettingsProvider simpleSettingsProvider = new SimpleSettingsProvider();
            IPhraseProvider simplePhraseProvider = new SimplePhraseProvider(simpleSettingsProvider.GetSettings().CurrentLanguage);
            IBoard consoleBoard = new ConsoleBoard(simpleSettingsProvider.GetSettings());
            GameMenu gm = new GameMenu(consoleBoard, simplePhraseProvider);
            Game g = new Game(consoleBoard, gm);
            g.Play();
        }
    }
}
