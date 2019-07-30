namespace Draw_Game.Components
{
    using System;
    using Draw_Game.Interfaces;
    using System.Collections.Generic;
    using System.Text;

    public class OutputWriter : IOutputWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void СlearСonsole()
        {
            Console.Clear();
        }
    }
}
