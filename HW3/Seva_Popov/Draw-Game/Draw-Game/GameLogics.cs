namespace Draw_Game
{    
using System;
using System.Collections.Generic;
using System.Text;
using Draw_Game.Interfaces;

   public class GameLogics
    {
        private readonly IInputReader reader;
        private readonly IOutputWriter writer;
        private readonly ISettingsProvider settingsProvider;
        private readonly IPhraseProvider phraseProvider;

        public GameLogics(IInputReader reader, IOutputWriter writer, ISettingsProvider settingsProvider, IPhraseProvider phraseProvider)
        {
            this.reader = reader;
            this.writer = writer;
            this.phraseProvider = phraseProvider;
            this.settingsProvider = settingsProvider;
        }

        public void RunGame()
        {
           this.writer.Write("Eeeee boy");
        }
    }
}
