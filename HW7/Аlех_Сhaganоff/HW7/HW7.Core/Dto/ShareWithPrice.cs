using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Dto
{
    public class ShareWithPrice
    {
        public int TraderId { get; set; }
        public int ShareId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
