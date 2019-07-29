// <copyright file="Program.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw
{
    using ConsoleDraw.Interfaces;
    using ConsoleDraw.Provider;

    /// <summary>
    /// Program.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            IInputOutputDevice iODevice = new ConsoleIO();

            Game game = new Game(
                settingsProvider: new JsonSettings(),
                iOProvider: iODevice,
                phraseProvider: new JsonPhraseProvider(),
                board: new Board(iODevice),
                figureProvider: new FigureProvider());

            game.Start();
        }
    }
}
