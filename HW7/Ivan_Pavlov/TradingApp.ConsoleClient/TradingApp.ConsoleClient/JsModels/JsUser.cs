using System.Collections.Generic;

namespace TradingApp.ConsoleClient.JsModels
{
    public class JsUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<JsPortfolio> UsersShares { get; set; }

        public JsUser()
        {
            UsersShares = new List<JsPortfolio>();
        }

        public override string ToString()
        {
            return $"{Name} {SurName} {Phone}";
        }
    }
}
