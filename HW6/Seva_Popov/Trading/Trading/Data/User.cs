using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trading.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; set; }

        public List<UserShare> UserShares { get; set; }

        public User()
        {
            UserShares = new List<UserShare>();
        }
    }
}
