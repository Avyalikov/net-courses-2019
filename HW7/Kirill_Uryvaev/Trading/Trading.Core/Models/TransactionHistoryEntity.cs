using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core
{
    public class TransactionHistoryEntity
    {
        public int TransactionID { get; set; }

        public int BuyerClientID { get; set; }

        public int SellerClientID { get; set; }

        public int ShareID { get; set; }

        public int Amount { get; set; }

        public decimal SumOfOperation { get; set; }

        public DateTime DateTime { get; set; }
    }
}
