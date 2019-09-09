namespace TradingApp.Core.ProxyForServices
{
    using TradingApp.Core.Logger;
    using System;
    using System.Collections.Generic;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;
    using TradingApp.Core.ServicesInterfaces;

    public class UsersProxy : IUsersService
    {
        private readonly UsersService usersService;

        public UsersProxy(UsersService usersService)
        {
            this.usersService = usersService;
        }


        public int AddNewUser(UserInfo args)
        {
            try
            {
                int id = this.usersService.AddNewUser(args);
                Logger.Log.Info($"Добавлен новый пользоватеель {args.SurName} {args.Name}");
                return id;
            }
            catch (ArgumentException ex)
            {
                Logger.Log.Error(ex.Message);
                return -1;
            }
        }

        public void ChangeUserBalance(int id, decimal value)
        {
            this.usersService.ChangeUserBalance(id, value);
            Logger.Log.Info($"Изменение баланса у пользователя с id {id} на сумму в {value}");
        }

        public ICollection<UserEntity> GetAllUsers()
        {
            return this.usersService.GetAllUsers();
        }

        public ICollection<UserEntity> GetAllUsersWithNegativeBalance()
        {
            return this.usersService.GetAllUsersWithNegativeBalance();
        }

        public ICollection<UserEntity> GetAllUsersWithZero()
        {
            return this.usersService.GetAllUsersWithZero();
        }

        public UserEntity GetCustomer(int userId, int sellerId)
        {
            try
            {
                return this.usersService.GetCustomer(userId, sellerId);
            }
            catch (ArgumentException ex)
            {
                Logger.Log.Error(ex.Message);
                return null;
            }
        }

        public UserEntity GetSeller(int userId)
        {
            try
            {
                return this.usersService.GetSeller(userId);
            }
            catch(ArgumentException ex)
            {
                Logger.Log.Error(ex.Message);
                return null;
            }
        }

        public UserEntity GetUserById(int id)
        {
            try
            {
                return this.usersService.GetUserById(id);
            }
            catch (ArgumentException ex)
            {
                Logger.Log.Error(ex.Message);
                return null;
            }
        }
    }
}
