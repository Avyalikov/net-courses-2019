//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace ConsoleDrawGame
{
    using Components;
    using Interfaces;

    /// <summary>
    /// This class contains an entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method is an entry point.
        /// </summary>
        private static void Main()
        {
            IInputOutput inputOutputComponent = new ConsoleInputOutput();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new PhraseProvider();
            IBoard board = new Board();
            IFigureProvider figureProvider = new FigureComponent();

            Game game = new Game(settingsProvider, inputOutputComponent, phraseProvider, board, figureProvider);
            game.Run();
        }
    }
}
