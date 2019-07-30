// <copyright file="program.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    /// <summary>
    /// class with the entry point
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        internal static void Main()
        {
            ISettingsProvider settingsProvider = new JsonSettingsProvider();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(settingsProvider);
            IInputOutputDevice inputOutputDevice = new ConsoleIODevice();
            IBoard board = new Board();
            IDrawOnBoard drawOnBoard = new DrawOnBoard();
            var game = new Game(
                phraseProvider: phraseProvider,
                inputOutputDevice: inputOutputDevice,
                settingsProvider: settingsProvider,
                board: board,
                drawOnBoard: drawOnBoard);
            game.Run();
        }
    }
}
