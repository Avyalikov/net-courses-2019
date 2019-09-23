using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Dto
{
    public class ShareToUpdate
    {
        [Required]
        public int ShareId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
