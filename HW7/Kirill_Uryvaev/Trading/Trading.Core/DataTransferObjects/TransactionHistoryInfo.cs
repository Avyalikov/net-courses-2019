using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.DataTransferObjects
{
    public class TransactionHistoryInfo
    {
        public int BuyerClientID { get; set; }

        public int SellerClientID { get; set; }

        public int ShareID { get; set; }

        public int Amount { get; set; }

        public decimal SumOfOperation { get; set; }

        public DateTime DateTime { get; set; }
    }
}
