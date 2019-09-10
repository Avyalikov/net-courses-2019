using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Intefaces
{
    public interface ILinkProcessorProvider
    {
        string ProcessLink (string link, int recursionLevel, string startingUrl, object storageLock);
    }
}
