namespace Trading.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    [Table("Users")]
    class User
    {
        [Key]
        public int Id { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; set; }
        
        public virtual ICollection<UserStocks> UserStocks { get; set; }

        public User()
        {
            UserStocks = new List<UserStocks>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format($"SurName {SurName} Name {Name} Balance = {Balance}"));
            if (UserStocks.Count == 0) sb.AppendLine("\t stocks don't found");
            else
            {
                foreach (var stock in UserStocks)
                {
                    sb.AppendLine(string.Format($"\t {stock.ToString()}"));
                }
            }
            return sb.ToString();
        }
    }
}
