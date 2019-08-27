namespace Traiding.Core.Models
{
    public class SharesNumberEntity
    {
        public int Id { get; set; }
        public virtual ClientEntity Client { get; set; }
        public virtual ShareEntity Share { get; set; }
        public int Number { get; set; }
    }
}
