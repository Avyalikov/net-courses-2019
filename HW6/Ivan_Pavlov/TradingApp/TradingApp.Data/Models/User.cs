namespace TradingApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<UserShare> UserShare { get; set; }

        public User()
        {
            UserShare = new List<UserShare>();
        }
    }
}
