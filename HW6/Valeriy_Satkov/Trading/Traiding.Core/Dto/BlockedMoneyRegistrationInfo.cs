namespace Traiding.Core.Dto
{
    using Traiding.Core.Models;

    public class BlockedMoneyRegistrationInfo
    {
        public BalanceEntity ClientBalance { get; set; }
        public OperationEntity Operation { get; set; }
        public decimal Total { get; set; }
    }
}
