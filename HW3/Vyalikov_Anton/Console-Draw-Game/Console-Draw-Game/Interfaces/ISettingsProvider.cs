using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Draw_Game.Interfaces
{
    interface ISettingsProvider
    {
        void ParseXML();

        string GetSetting(string xmlKey);
    }
}
