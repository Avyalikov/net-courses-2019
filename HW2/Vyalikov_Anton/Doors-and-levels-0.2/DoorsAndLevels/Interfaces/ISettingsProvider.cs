using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevels.Interfaces
{
    interface ISettingsProvider
    {
        void ParseXML();

        string GetSetting(string xmlKey);
    }
}
