using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traiding.ConsoleApp.Dto
{
    public class OperationInputData
    {
        public int CustomerId { get; set; }
        public int SellerId { get; set; }
        public int ShareId { get; set; }
        public int RequiredSharesNumber { get; set; }
    }
}
