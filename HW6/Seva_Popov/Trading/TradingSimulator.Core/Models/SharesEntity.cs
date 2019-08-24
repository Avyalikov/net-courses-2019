using System;
using System.Collections.Generic;
using System.Text;

namespace TradingSimulator.Core.Models
{
    public class SharesEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

       // public List<UserShare> UserShares { get; set; }

       // public Share()
        //{
        //    UserShares = new List<UserShare>();
        //}
    }
}
