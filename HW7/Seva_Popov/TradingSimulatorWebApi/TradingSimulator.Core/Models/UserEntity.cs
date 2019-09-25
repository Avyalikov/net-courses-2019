using System;
using System.Collections.Generic;
using System.Text;

namespace TradingSimulator.Core.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; set; }

        public List<UserAndSharesEntity> UsersAndShares { get; set; }

        public UserEntity()
        {
            UsersAndShares = new List<UserAndSharesEntity>();
        }
    }
}
