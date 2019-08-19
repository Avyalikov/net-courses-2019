namespace TradingSimulator.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradingSimulator.Model;

    public class Transaktion
    {
        public string sellerName;
        public string sellerSurname;
        public string buyerName;
        public string buyerSurname;
        public string shareName;
        public int quantity;

        public override string ToString()
        {
            return $"{sellerName} {sellerSurname} sells {quantity} quantity of {shareName} shares to {buyerName} {buyerSurname}";
        }
    }
}