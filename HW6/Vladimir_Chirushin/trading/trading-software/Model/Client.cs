namespace trading_software
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        [Key]
        public int ClientID { get; set; }
        [MaxLength(22)]
        [Required]
        public string Name { get; set; }
        [MaxLength(14)]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public decimal Balance { get; set; }
        public virtual List<BlockOfShares> blockOfShares { get; set; }
    }
}