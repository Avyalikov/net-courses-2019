namespace Draw_Game.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IOutputWriter
    {
        void Write(string message);
        void СlearСonsole();
    }
}
