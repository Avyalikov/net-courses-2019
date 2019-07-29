using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Draw_Game.Interfaces
{
    interface IPhraseProvider
    {
        void ParseXML(string requireLang);
        string GetMessage(string key);
    }
}
