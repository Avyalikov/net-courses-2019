using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.DataTransferObjects
{
    public class ClientsSharesInfo
    {
        public int ClientID { get; set; }
        public int ShareID { get; set; }
        public int Amount { get; set; }
    }
}
