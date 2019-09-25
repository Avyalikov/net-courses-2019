using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TradingSimulator.Core.Models
{
    public class AddingSharesToThUserEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public int ShareId { get; set; }
        public SharesEntity Share { get; set; }

        public int AmountStocks { get; set; }
    }
}
