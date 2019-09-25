namespace TradingApp.ConsoleClient.JsonModels
{
    using System.Collections.Generic;

    public class JsonUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<JsonPortfolio> UsersShares { get; set; }

        public JsonUser()
        {
            UsersShares = new List<JsonPortfolio>();
        }

        public override string ToString()
        {
            return $"{Name} {SurName} {Phone}";
        }
    }
}
