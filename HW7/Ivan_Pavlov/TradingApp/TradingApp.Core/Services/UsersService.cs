namespace TradingApp.Core.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.ServicesInterfaces;

    public class UsersService : IUsersService
    {
        private readonly IUserTableRepository repo;

        public UsersService(IUserTableRepository repo)
        {
            this.repo = repo;
        }
                
        public int AddNewUser(UserInfo args)
        {
            var UserToAdd = new UserEntity()
            {
                Name = args.Name,
                SurName = args.SurName,
                Balance = args.Balance,
                Phone = args.Phone
            };

            if (this.repo.Contains(UserToAdd))
            {
                throw new ArgumentException("Не уникальный пользователь");
            }

            repo.Add(UserToAdd);

            repo.SaveChanges();

            return UserToAdd.Id;
        }

        public ICollection<UserEntity> GetAllUsers()
        {
            return this.repo.GetAllUsers();
        }

        public UserEntity GetUserById(int id)
        {
            UserEntity user;
            if ((user = repo.GetUserById(id)) == null)
            {
                throw new ArgumentException("Данный пользователь не найден");
            }

            return user;
        }

        public ICollection<UserEntity> GetAllUsersWithZero()
        {
            return this.repo.GetAllUsersWithZero();
        }

        public ICollection<UserEntity> GetAllUsersWithNegativeBalance()
        {
            return this.repo.GetAllUsersWithNegativeBalance();
        }

        public void ChangeUserBalance(int id, decimal value)
        {
            var user = this.repo.GetUserById(id);
            user.Balance = user.Balance + value;
            repo.SaveChanges();
        }

        public UserEntity GetSeller(int userId)
        {
            if (repo.Count() == 0)
                throw new ArgumentException("Нет зарегестрированных пользователей");
            UserEntity seller = repo.GetUserById(userId);
            if (seller == null)
                throw new ArgumentException($"Нет пользователя с ID {userId}");
            else if (seller.UsersShares == null)
                throw new ArgumentException($"{seller.SurName} {seller.Name} не имеет акций для продажи");
            return seller;
        }

        public UserEntity GetCustomer(int userId, int sellerId)
        {
            if (userId == sellerId)
                throw new ArgumentException("Покупатель и продавец - один персонаж");
            UserEntity customer = repo.GetUserById(userId);
            if (customer == null)
                throw new ArgumentException($"Нет пользователя с ID {userId}");
            return customer;
        }

        public void Update(int id, UserInfo entity)
        {
            UserEntity user = repo.GetUserById(id);
            user.Name = entity.Name ?? user.Name;
            user.SurName = entity.SurName ?? user.SurName;
            user.Phone = entity.Phone ?? user.Phone;
            // балан таким образом менять нельзя
            repo.SaveChanges();
        }

        public void Remove(int id)
        {
            repo.Remove(id);
            repo.SaveChanges();
        }
    }
}
