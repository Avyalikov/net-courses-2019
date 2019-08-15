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

        [Required, MaxLength(50), MinLength(5)]
        public string CompanyName { get; set; }

        [Required, ForeignKey("ShareTypeId")]
        public ShareType ShareType { get; set; }
    }
}
