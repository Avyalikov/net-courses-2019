using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSoftware.Core.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public DateTime dateTime { get; set; }
        public virtual int SellerID { get; set; }
        public virtual int BuyerID { get; set; }
        public virtual int ShareID { get; set; }
        public int Amount { get; set; }
    }
}
