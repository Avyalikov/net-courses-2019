using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.Core.Services
{
    public class UserService
    {
        private readonly IUserTableRepository userTableRepository;

        public UserService(IUserTableRepository userTableRepository)
        {
            this.userTableRepository = userTableRepository;
        }

        public int RegisterNewUser(UserRegistrationInfo args)
        {
            var entityToAdd = new UserEntity() { Surname = args.Surname, Name = args.Name, Balance = 10000, Phone = args.Phone };

            if (this.userTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This user has been registered. Can't continue");
            }

            this.userTableRepository.Add(entityToAdd);

            this.userTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public UserEntity GetUser(int userId)
        {
            if (!this.userTableRepository.ContainsById(userId))
            {
                throw new ArgumentException("Can't get user by this id. May it has not been registred.");
            }

            return this.userTableRepository.Get(userId);
        }
    }
}
