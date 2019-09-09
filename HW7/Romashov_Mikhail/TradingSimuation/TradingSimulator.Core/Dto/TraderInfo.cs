using System;
using System.Collections.Generic;
using System.Text;

namespace TradingSimulator.Core.Dto
{
    public class TraderInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
