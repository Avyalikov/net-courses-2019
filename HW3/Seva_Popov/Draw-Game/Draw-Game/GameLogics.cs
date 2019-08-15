namespace Draw_Game
{    
using System;
using System.Collections.Generic;
using System.Text;
using Draw_Game.Interfaces;

    public delegate void Draw(IBoard board);

    public class GameLogics
    {
        private readonly IInputReader reader;
        private readonly IOutputWriter writer;
        private readonly ISettingsProvider settingsProvider;
        private readonly IPhraseProvider phraseProvider;
        private readonly IBoard board;
        private GameSettings gameSettings;

        public GameLogics(
            IInputReader reader, 
            IOutputWriter writer, 
            ISettingsProvider settingsProvider, 
            IPhraseProvider phraseProvider, 
            GameSettings gameSettings, 
            IBoard board)
        {
            this.reader = reader;
            this.writer = writer;
            this.phraseProvider = phraseProvider;
            this.settingsProvider = settingsProvider;         
            this.gameSettings = gameSettings;
            this.board = board;
            this.gameSettings = this.settingsProvider.GetGameSettings();
        }

        public void RunGame()
        {

            Console.WriteLine(settingsProvider.GetGameSettings().Length);
            //board.SizeX = settingsProvider.GetGameSettings().Length;
            //board.SizeY = settingsProvider.GetGameSettings().Width;
            //Console.WriteLine(phraseProvider.GetPhrase("Start"));
            //Console.WriteLine(phraseProvider.GetPhrase("Select1234"));
            //Console.WriteLine(phraseProvider.GetPhrase("Select0")+ settingsProvider.GetGameSettings().ExitCode);
            //Console.WriteLine(phraseProvider.GetPhrase("#"));
            //Console.ReadKey();
            //writer.СlearСonsole();

            //while (true)
            //{
            //    this.board.Create(board);
            //    Draw draw = delegate (IBoard board) { };
            //    Console.WriteLine(phraseProvider.GetPhrase("1"));
            //    Console.WriteLine(phraseProvider.GetPhrase("2"));
            //    Console.WriteLine(phraseProvider.GetPhrase("3"));
            //    Console.WriteLine(phraseProvider.GetPhrase("4"));
            //    string st = reader.ReadLine();

            //    if (st.Equals("1"))
            //    {
            //        draw += this.board.HorizontalLine;
            //        draw(board);
            //    }

            //    if (st.Equals("2"))
            //    {
            //        draw += this.board.VerticalLine;
            //        draw(board);
            //    }

            //    if (st.Equals("3"))
            //    {
            //        draw += this.board.SimpleDot;
            //        draw(board);
            //    }

            //    if (st.Equals("4"))
            //    {
            //        draw += this.board.AnotherShape;
            //        draw(board);
            //    }
            //    if (st.Equals(settingsProvider.GetGameSettings().ExitCode))
            //    {
            //        break;
            //    }
            //}
        }
    }
}
