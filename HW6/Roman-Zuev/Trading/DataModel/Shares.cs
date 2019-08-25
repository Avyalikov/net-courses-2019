using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.DataModel
{
    public class Shares
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SharesID { get; set; }

        [Required]
        public string SharesType { get; set; }

        [Required]
        [Range(0,1000000000)]
        public decimal Price { get; set; }
    }
}
