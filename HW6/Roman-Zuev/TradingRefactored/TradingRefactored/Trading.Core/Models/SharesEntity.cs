using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Models
{
    public class SharesEntity
    {
        public int Id { get; set; }
        public string SharesType { get; set; }
        public decimal Price { get; set; }
    }
}
