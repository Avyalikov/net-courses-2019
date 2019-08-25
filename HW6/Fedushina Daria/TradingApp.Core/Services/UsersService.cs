using System;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;

namespace TradingApp.Core.Services
{
    public class UsersService
    {
        private readonly IUserTableRepository userTableRepository;

        public UsersService(IUserTableRepository userTableRepository)
        {
            this.userTableRepository = userTableRepository;
        }

        public string RegisterNewUser(UserRegistrationInfo args)
        {
            var entityToAdd = new UserEntity()
            {
                CreatedAt = DateTime.Now,
                Name = args.Name,
                Surname = args.Surname,
                PhoneNumber = args.PhoneNumber

            };
            if (this.userTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This user has been already registered.Can't continue");
            }
            this.userTableRepository.Add(entityToAdd);

            this.userTableRepository.SaveChanges();
            return entityToAdd.ID;
        }

        public UserEntity GetUser(string userId)
        {
            if (!this.userTableRepository.Contains(userId))
            {
                throw new ArgumentException("Can't find user with this Id. May it hasn't been registered");
            }
            return this.userTableRepository.Get(userId);
            
        }
    }
}