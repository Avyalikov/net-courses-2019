// <copyright file="Program.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw
{
    using Components;
    using Interfaces;

    /// <summary>
    /// class with the entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        public static void Main()
        {
            ISettingsProvider settingsProvider = new JsonSettingsProvider();
            IInputOutputDevice inputOutputDevice = new ConsoleInputOutputDevice();            
            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            IBoard board = new DashBoard();
            IFigureProvider figureProvider = new FigureProvider();

            new Game(settingsProvider, inputOutputDevice, phraseProvider, board, figureProvider) { }.Start();
        }
    }
}
