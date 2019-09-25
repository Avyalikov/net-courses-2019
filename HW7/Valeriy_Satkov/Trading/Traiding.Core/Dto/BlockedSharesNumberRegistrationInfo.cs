namespace Traiding.Core.Dto
{
    using Traiding.Core.Models;

    public class BlockedSharesNumberRegistrationInfo
    {
        public SharesNumberEntity ClientSharesNumber { get; set; }
        public OperationEntity Operation { get; set; }
        //public ShareEntity Share { get; set; }
        //public string ShareTypeName { get; set; }
        //public decimal Cost { get; set; }
        public int Number { get; set; }
    }
}
