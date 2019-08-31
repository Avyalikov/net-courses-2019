using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApiClient.Devices
{
    public interface IOutputDevice
    {
        void WriteLine(string outputString);

        void Clear();
    }
}
