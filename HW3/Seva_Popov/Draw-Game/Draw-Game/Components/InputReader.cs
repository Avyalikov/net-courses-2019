namespace Draw_Game.Components
{
    using Draw_Game.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InputReader : IInputReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
