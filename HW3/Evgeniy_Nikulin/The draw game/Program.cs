//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game
{
    using Components;
    using Interfaces;

    /// <summary>
    /// Entering point class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entering method
        /// </summary>
        public static void Main()
        {
            ISettingsProvider settingsProvider = new SettingsProvider();
            var settings = settingsProvider.Get();
            
            IInputOutput inputOutput = new ConsoleIOModule();

            IBoard board;
            try
            {
                board = new Board(inputOutput, settings.BorderWidth, settings.BorderHeight, settings.BorserStile);
            }
            catch (System.Exception e)
            {
                inputOutput.Print("Please set the correct board size. " + e.Message);
                inputOutput.Input();
                return;
            }

            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            phraseProvider.Init(settings.Language);

            IFigureManager figureManager = new FigureManager(inputOutput, settings.FigureStile);

            Game game = new Game(
                st: settings, 
                board: board,
                io: inputOutput,
                phraseProvider: phraseProvider,
                figureManager: figureManager);

            game.Run();
        }
    }
}