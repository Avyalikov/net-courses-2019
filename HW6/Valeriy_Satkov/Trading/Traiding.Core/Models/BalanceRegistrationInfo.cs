using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traiding.Core.Models
{
    public class BalanceRegistrationInfo
    {
        public ClientEntity Client { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
    }
}
