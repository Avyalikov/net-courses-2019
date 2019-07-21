using System;
using System.Collections.Generic;
using System.Text;

namespace Doors_and_levels_game.Interfaces
{
    public interface ISettingsProvider
    {
        GameSettings Get();
    }
}