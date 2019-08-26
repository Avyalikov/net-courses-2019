namespace TradingApp.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class TraderService
    {
        private readonly IRepository<TraderEntity> traderTableRepository;
        public TraderService(IRepository<TraderEntity> traderTableRepository) => this.traderTableRepository = traderTableRepository;

        public virtual List<TraderEntity> GetTradersFromOrangeZone()
        {
            return this.traderTableRepository.GetAll().Where(t => t.Balance == 0).ToList();
        }
        public virtual List<TraderEntity> GetTradersFromBlackZone()
        {
            return this.traderTableRepository.GetAll().Where(t => t.Balance < 0).ToList();
        }
        public virtual List<TraderEntity> GetAllTraders()
        {
            return this.traderTableRepository.GetAll();
        }
        public virtual int RegisterNewUser(TraderInfo info)
        {
            ValidateTraderInfo(info);

            var entityToAdd = new TraderEntity()
            {
                FirstName = info.FirstName,
                LastName = info.LastName,
                PhoneNumber = info.PhoneNumber,
                Balance = info.Balance
            };

            ValidatePossibleTraderCollisions(entityToAdd);


            this.traderTableRepository.Add(entityToAdd);

            this.traderTableRepository.SaveChanges();

            return entityToAdd.Id;
        }
        private void ValidateTraderInfo(TraderInfo info)
        {
            if (!info.FirstName.All(char.IsLetter)
                || info.FirstName == string.Empty
                || !info.LastName.All(char.IsLetter)
                || info.LastName == string.Empty
                || !info.PhoneNumber.All(char.IsDigit)
                || info.PhoneNumber == string.Empty)
            {
                throw new ArgumentException("User can't be added. Check your input please.");
            }
        }
        private void ValidatePossibleTraderCollisions(TraderEntity entity)
        {
            if (this.traderTableRepository.Contains(entity))
            {
                throw new Exception("This user is already in database.");
            }
        }
    }
}
