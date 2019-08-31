using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApiClient.Devices
{
    using System;

    public interface IInputDevice
    {
        string ReadLine();

        ConsoleKeyInfo ReadKey();
    }
}
