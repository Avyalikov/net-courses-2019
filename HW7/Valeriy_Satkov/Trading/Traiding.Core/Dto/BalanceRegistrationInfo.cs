namespace Traiding.Core.Dto
{
    using Traiding.Core.Models;

    public class BalanceRegistrationInfo
    {
        public ClientEntity Client { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
    }
}
