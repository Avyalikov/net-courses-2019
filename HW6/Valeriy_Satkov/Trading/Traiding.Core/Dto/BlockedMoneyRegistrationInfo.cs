using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.Core.Dto
{
    public class BlockedMoneyRegistrationInfo
    {
        public BalanceEntity ClientBalance { get; set; }
        public OperationEntity Operation { get; set; }
        public decimal Total { get; set; }
    }
}
