using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Trading.Data
{
    public class UserShare
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ShareId { get; set; }
        public Share Share { get; set; }

        public int AmountStocks { get; set; }
    }
}
