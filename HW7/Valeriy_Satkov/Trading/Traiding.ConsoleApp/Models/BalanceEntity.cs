namespace Traiding.ConsoleApp.Models
{
    public class BalanceEntity
    {
        public int Id { get; set; }        
        public virtual ClientEntity Client { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
    }
}
