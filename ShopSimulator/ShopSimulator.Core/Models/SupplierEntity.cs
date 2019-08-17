using System;

namespace ShopSimulator.Core.Models
{
    public class SupplierEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }
    }
}
