﻿namespace Draw_Game
{
using System;
using Draw_Game.Components;
using Draw_Game.Interfaces;

    //private delegate void Draw(IBoard board);

    public class Program
    {
       private static void Main(string[] args)
        {
            IInputReader reader = new InputReader();
            IOutputWriter writer = new OutputWriter();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new PhraseProvider();

            GameLogics gameLogics = new GameLogics(reader, writer, settingsProvider, phraseProvider);
            gameLogics.RunGame();
        }
    }
}
