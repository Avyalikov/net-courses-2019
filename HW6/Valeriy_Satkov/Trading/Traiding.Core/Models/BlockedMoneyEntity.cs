using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traiding.Core.Models
{
    public class BlockedMoneyEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } // Debit time
        public virtual BalanceEntity ClientBalance { get; set; }
        public virtual OperationEntity Operation { get; set; }
        public virtual ClientEntity Customer { get; set; }
        public decimal Total { get; set; }
    }
}
