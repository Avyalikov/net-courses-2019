namespace TradingApp.Core.Models
{
    using System.Collections.Generic;

    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<PortfolioEntity> UsersShares { get; set; }

        public UserEntity()
        {
            UsersShares = new List<PortfolioEntity>();
        }
    }
}
