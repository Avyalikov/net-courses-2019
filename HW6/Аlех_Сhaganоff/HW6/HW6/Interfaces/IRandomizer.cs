using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6.Interfaces
{
    public interface IRandomizer
    {
        void Randomize(TradingContext context, IOutputProvider outputProvider);
    }
}
