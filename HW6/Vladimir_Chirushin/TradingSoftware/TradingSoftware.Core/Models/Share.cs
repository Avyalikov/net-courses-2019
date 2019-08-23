using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSoftware.Core.Models
{
    public class Share
    {
        public int ShareID { get; set; }
        public string ShareType { get; set; }
        public decimal Price { get; set; }
    }
}
