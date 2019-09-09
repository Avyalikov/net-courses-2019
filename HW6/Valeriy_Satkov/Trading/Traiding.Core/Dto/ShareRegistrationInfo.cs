namespace Traiding.Core.Dto
{
    using Traiding.Core.Models;

    public class ShareRegistrationInfo
    {
        public string CompanyName { get; set; }
        public ShareTypeEntity Type { get; set; }
        public bool Status { get; set; }
    }
}
