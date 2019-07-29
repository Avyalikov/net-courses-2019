namespace Draw_Game
{    
using System;
using System.Collections.Generic;
using System.Text;
using Draw_Game.Interfaces;

    public delegate void CoundDel(string desm, int a, int b);

    public class GameLogics
    {
        private readonly IInputReader reader;
        private readonly IOutputWriter writer;
        private readonly ISettingsProvider settingsProvider;
        private readonly IPhraseProvider phraseProvider;
        private readonly IBoard board;
        private GameSettings gameSettings;

        public GameLogics(IInputReader reader, IOutputWriter writer, ISettingsProvider settingsProvider, IPhraseProvider phraseProvider, GameSettings gameSettings, IBoard board)
        {
            this.reader = reader;
            this.writer = writer;
            this.phraseProvider = phraseProvider;
            this.settingsProvider = settingsProvider;         
            this.gameSettings = gameSettings;
            this.board = board;
        }

        public void RunGame()
        {
            CoundDel coundDel = writer.WriteAt;
            board.Create(coundDel);
            board.HorizontalLine(coundDel);
            board.SimpleDot(coundDel);
            board.VerticalLine(coundDel);
            board.Curve(coundDel);
        }
    }
}
