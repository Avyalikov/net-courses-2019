using System;
using System.Collections.Generic;

namespace stockSimulator.Core.Models
{
    public class ClientEntity : IEquatable<ClientEntity>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateAt { get; set; }
        public decimal AccountBalance { get; set; }

        public virtual ICollection<StockOfClientsEntity> Stocks { get; set; }

        public bool Equals(ClientEntity other)
        {
            if(this.Name == other.Name
                && this.Surname == other.Surname
                && this.PhoneNumber == other.PhoneNumber
                && this.AccountBalance == other.AccountBalance)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"ID: {this.ID}, Name: {this.Name}, Surname: {this.Surname}, Phonenumber: {this.PhoneNumber}, " +
                $"Registered {this.CreateAt.ToString()}, Balance: {this.AccountBalance}";
        }
    }
}
