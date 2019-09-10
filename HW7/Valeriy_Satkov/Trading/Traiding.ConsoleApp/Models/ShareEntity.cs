namespace Traiding.ConsoleApp.Models
{
    using System;

    public class ShareEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CompanyName { get; set; }
        public virtual ShareTypeEntity Type { get; set; }
        public bool Status { get; set; }
    }
}
