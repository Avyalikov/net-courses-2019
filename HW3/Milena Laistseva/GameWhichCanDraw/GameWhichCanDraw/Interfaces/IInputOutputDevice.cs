using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWhichCanDraw
{
    public interface IInputOutputDevice
    {
        string ReadInput();
        char ReadKey();
        void WriteOutput(string dataToOutput);
    }
}
