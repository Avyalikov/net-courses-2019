using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.Core.Dto
{
    public class BlockedSharesNumberRegistrationInfo
    {
        public SharesNumberEntity ClientSharesNumber { get; set; }
        public OperationEntity Operation { get; set; }
        public ShareEntity Share { get; set; }
        public string ShareTypeName { get; set; }
        public decimal Cost { get; set; }
        public int Number { get; set; }
    }
}
