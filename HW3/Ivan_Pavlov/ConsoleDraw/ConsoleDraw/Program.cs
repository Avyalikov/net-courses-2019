// <copyright file="Program.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw
{
    using ConsoleDraw.Provider;

    /// <summary>
    /// Program.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            Game game = new Game(
                settingsProvider: new JsonSettings(),
                iOProvider: new ConsoleIO(),
                phraseProvider: new JsonPhraseProvider(),
                board: new Board(),
                figureProvider: new FigureProvider());

            game.Start();
        }
    }
}
