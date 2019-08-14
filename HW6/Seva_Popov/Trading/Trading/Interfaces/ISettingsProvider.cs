using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Interfaces
{
    interface ISettingsProvider
    {
        GameSettings GetGameSettings();
    }
}
