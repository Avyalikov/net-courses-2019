using System;
using System.Collections.Generic;
using System.Text;

namespace TradingSimulator.Interfaces
{
    public interface ISettingsProvider
    {
        GameSettings Get();
    }
}