namespace Trading.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ShareType
    {
        [Key]
        public int ShareTypeId { get; set; }

        [Required, MaxLength(20), MinLength(2)]
        public string Name { get; set; }

        [Required]
        public decimal Cost { get; set; }
    }
}
