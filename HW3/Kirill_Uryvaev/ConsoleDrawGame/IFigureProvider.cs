using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDrawGame
{
    interface IFigureProvider
    {
        Dictionary<string, Draw> GetFigures();
    }
}
