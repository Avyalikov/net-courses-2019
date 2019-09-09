using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.DataModel
{
    public class Client
    {
        public int ClientID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string PhoneNumber { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required, MaxLength(10)]
        public string Zone { get; set; }
        public ICollection<ClientShares> ClientShares { get; set; }
        public Client()
        {
            ClientShares = new List<ClientShares>();
        }
    }
}

