using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traiding.Core.Models
{
    public class ShareEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CompanyName { get; set; }
        public virtual ShareTypeEntity Type { get; set; }
        public bool Status { get; set; }
    }
}
