using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traiding.Core.Models
{
    public class SharesNumberRegistrationInfo
    {
        public ClientEntity Client { get; set; }
        public ShareEntity Share { get; set; }
        public int Number { get; set; }
    }
}
