// <copyright file="Program.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    using System;

    /// <summary>
    /// Class with entering point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        public static void Main(string[] args)
        {
            ISettingsProvider settingsProvider = new JsonSettingsProvider();
            GameSettings gameSettings = settingsProvider.GetGameSettings();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(gameSettings.Language);
            IInputOutputDevice inputOutputDevice = new ConsoleInputOutputDevice();
            IBoard board = new ConsoleBoard();
            IDrawOnBoard drawOnBoard = new DrawOnConsoleBoard();

            Game drawingGame = new Game(board, phraseProvider, inputOutputDevice, drawOnBoard, settingsProvider);
            drawingGame.Run();

            Console.ReadKey();
        }
    }
}
