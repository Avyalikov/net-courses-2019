using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSoftware.Core.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public virtual List<BlockOfShares> blockOfShares { get; set; }
    }
}
