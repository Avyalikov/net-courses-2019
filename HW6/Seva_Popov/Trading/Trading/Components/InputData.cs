using System;
using System.Collections.Generic;
using System.Text;
using Trading.Interfaces;

namespace Trading.Components
{
    class InputData : IInputData
    {
        public string ReadInput()
        {
           return Console.ReadLine();
        }
    }
}
