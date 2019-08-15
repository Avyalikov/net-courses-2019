namespace Trading.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required, MaxLength(20), MinLength(2)]
        public string LastName { get; set; }

        [Required, MaxLength(20), MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}
