using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8_EF.Models
{
    public class Links
    {
        [Key]
        [MaxLength(250)]
        public string Link { get; set; }
        [Required]
        public int IterationId { get; set; }
    }
}
