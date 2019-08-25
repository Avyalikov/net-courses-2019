using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Models
{
    public class ClientSharesEntity
    {
        public int Id { get; set; }
        public virtual SharesEntity Shares { get; set; }
        public int Quantity { get; set; }
        public virtual ClientEntity Client { get; set; }
    }
}
