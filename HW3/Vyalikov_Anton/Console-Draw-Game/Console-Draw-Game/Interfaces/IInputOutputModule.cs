using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Draw_Game.Interfaces
{
    interface IInputOutputModule
    {
        string ReadInput();

        void WriteOutput(string outputData);

        void SetPosition(int x, int y);

        void Clear();
    }
}
