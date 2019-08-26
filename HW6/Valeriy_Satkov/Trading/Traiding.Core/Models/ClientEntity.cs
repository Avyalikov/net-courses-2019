namespace Traiding.Core.Models
{
    using System;

    public class ClientEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
}
