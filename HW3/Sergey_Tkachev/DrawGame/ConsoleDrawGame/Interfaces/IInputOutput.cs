using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame.Interfaces
{
    interface IInputOutput
    {
        /// <summary>Reds data and returns it.</summary>
        /// <returns></returns>
        string ReadInput();
        /// <summary>Reads and returns a char.</summary>
        /// <returns></returns>
        char ReadKey();
        /// <summary>Write data.</summary>
        /// <param name="dataToOutput">String to write.</param>
        void WriteOutput(string dataToOutput);
        /// <summary>Prints a string on X and Y coordinates in concole.</summary>
        /// <param name="s">String to print.</param>
        /// <param name="x">X coord.</param>
        /// <param name="y">Y coord.</param>
        void WriteAt(string s, int x, int y);
    }
}
