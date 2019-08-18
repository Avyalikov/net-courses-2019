using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.DataModel
{
    public class ClientShares
    {
        public int ClientSharesID { get; set; }
        [Required]
        public Shares Shares { get; set; }

        [Required, Range(0, 1000000)]
        public int Quantity { get; set; }
        public int? ClientID { get; set; }
        public Client Client { get; set; }
    }
}
