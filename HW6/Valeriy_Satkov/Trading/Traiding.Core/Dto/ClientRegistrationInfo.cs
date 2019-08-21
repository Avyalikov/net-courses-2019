using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traiding.Core.Dto
{
    public class ClientRegistrationInfo
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
}
