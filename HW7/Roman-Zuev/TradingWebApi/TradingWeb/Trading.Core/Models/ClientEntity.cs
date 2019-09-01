using System;
using System.Collections.Generic;

namespace Trading.Core.Models
{
    public class ClientEntity
    {
        public int Id { get; set; }
        public DateTime RegistationDateTime { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<ClientSharesEntity> Portfolio { get; set; }
    }
}