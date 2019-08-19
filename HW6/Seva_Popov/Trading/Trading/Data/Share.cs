using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trading.Data
{
    public class Share
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<UserShare> UserShares { get; set; }

        public Share()
        {
            UserShares = new List<UserShare>();
        }
    }
}
