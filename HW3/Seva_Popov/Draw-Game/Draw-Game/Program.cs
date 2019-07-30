namespace Draw_Game
{
    using System;
    using Draw_Game.Components;
    using Draw_Game.Interfaces;

    public class Program
    {
       private static void Main(string[] args)
        {
            IInputReader reader = new InputReader();
            IOutputWriter writer = new OutputWriter();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new PhraseProvider();
            GameSettings gameSettings = new GameSettings();
            IBoard board = new Board();

            GameLogics gameLogics = new GameLogics(reader, writer, settingsProvider, phraseProvider,gameSettings, board);
            gameLogics.RunGame();
        }
    }
}
