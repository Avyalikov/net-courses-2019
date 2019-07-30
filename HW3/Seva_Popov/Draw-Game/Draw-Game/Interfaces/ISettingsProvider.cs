namespace Draw_Game.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISettingsProvider
    {
         GameSettings GetGameSettings();
    }
}
