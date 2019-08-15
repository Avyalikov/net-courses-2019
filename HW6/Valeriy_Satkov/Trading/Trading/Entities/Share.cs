namespace Trading.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Share
    {
        [Key]
        public int ShareId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required, ForeignKey("ShareTypeId")]
        public int ShareType { get; set; }
    }
}
