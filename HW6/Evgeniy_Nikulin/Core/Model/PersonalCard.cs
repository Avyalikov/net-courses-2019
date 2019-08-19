namespace Core.Model
{
    using System.ComponentModel.DataAnnotations;

    public class PersonalCard
    {
        public int ID { get; set; }
        [MaxLength(128)]
        [Required]
        public string Name { get; set; }
        [MaxLength(128)]
        [Required]
        public string Surname { get; set; }
        [MaxLength(32)]
        [Required]
        public string Phone { get; set; }
    }
}