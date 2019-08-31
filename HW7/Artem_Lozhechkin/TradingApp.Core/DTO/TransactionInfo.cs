using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApp.Core.DTO
{
    public class TransactionInfo
    {
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int ShareId { get; set; }
    }
}
