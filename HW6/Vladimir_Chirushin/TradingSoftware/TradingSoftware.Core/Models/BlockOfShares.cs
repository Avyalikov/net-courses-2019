using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSoftware.Core.Models
{
    public class BlockOfShares
    {
        public virtual int ClientID { get; set; }
        public virtual int StockID { get; set; }
        public int Amount { get; set; }
    }
}
