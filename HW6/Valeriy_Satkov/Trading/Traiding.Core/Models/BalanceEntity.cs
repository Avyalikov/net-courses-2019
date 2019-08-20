using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traiding.Core.Models
{
    public class BalanceEntity
    {
        public int Id { get; set; }        
        public virtual ClientEntity Client { get; set; }
        public decimal Amount { get; set; }
    }
}
