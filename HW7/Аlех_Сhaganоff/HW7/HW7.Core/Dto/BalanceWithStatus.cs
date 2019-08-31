using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Dto
{
    public class BalanceWithStatus
    {
        public int TraderId { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }
    }
}
