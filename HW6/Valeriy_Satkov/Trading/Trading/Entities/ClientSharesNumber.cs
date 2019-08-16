namespace Trading.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ClientSharesNumber
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Client Client { get; set; }

        [Required]
        public virtual Share Share { get; set; }

        [Required]
        public int Number { get; set; }
    }
}
