namespace Draw_Game.Components
{
    using Draw_Game.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PhraseProvider : IPhraseProvider
    {
        public string GetPhrase(string phraseKey)
        {
            return "S";
        }
    }
}
