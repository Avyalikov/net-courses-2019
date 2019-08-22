namespace Traiding.Core.Dto
{
    using Traiding.Core.Models;

    public class SharesNumberRegistrationInfo
    {
        public ClientEntity Client { get; set; }
        public ShareEntity Share { get; set; }
        public int Number { get; set; }
    }
}
