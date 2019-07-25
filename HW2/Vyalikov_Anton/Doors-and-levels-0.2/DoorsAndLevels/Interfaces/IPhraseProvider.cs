using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevels.Interfaces
{
    interface IPhraseProvider
    {
        void ParseXML(string requireLang);
        string GetMessage(string key);
    }
}
