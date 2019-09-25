using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSimulator.Core.Dto
{
    public class RegInfo
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Balance { get; set; }

        public bool HaveStoks { get; set; }

        public string TypeOfStocks { get; set; }

        public int quantityOfStocks { get; set; }

        public decimal PriceOfStock { get; set; }
    }
}
