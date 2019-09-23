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
        public virtual void UpdateUser(TraderEntity traderEntity)
        {
            ValidateTraderInfo(traderEntity);
            ValidateTradersExistence(traderEntity.Id);
            var trader = this.traderTableRepository.GetById(traderEntity.Id);

            trader.FirstName = traderEntity.FirstName;
            trader.LastName = traderEntity.LastName;
            trader.PhoneNumber = traderEntity.PhoneNumber;
            trader.Balance = traderEntity.Balance;
            this.traderTableRepository.Save(trader);
        }
        public virtual void RemoveUser(TraderInfo info)
        {
            ValidateTraderInfo(info);

            var trader = this.traderTableRepository.GetByPredicate(t =>
                t.FirstName == info.FirstName &&
                t.LastName == info.LastName &&
                t.PhoneNumber == info.PhoneNumber &&
                t.Balance == info.Balance).FirstOrDefault();
            ValidateTradersExistence(trader);


            this.traderTableRepository.Delete(trader);
            this.traderTableRepository.SaveChanges();
        }
        public virtual string GetUserStatus(int id)
        {
            ValidateTradersExistence(id);

            var traderBalance = this.traderTableRepository.GetById(id).Balance;



            if (traderBalance > 0) return $"Balance: {traderBalance:0.00}, Status: Green";
            if (traderBalance == 0) return $"Balance: {traderBalance:0.00}, Status: Orange";
            else return $"Balance: {traderBalance:0.00}, Status: Black";
        }
        public virtual IEnumerable<string> GetUserLists(int top, int page)
        {
            ValidateContentForPages(top, page);
            return this.GetAllTraders().Skip((page - 1) * top).Take(top).Select(t => $"{t.FirstName} {t.LastName}");
        }
        private void ValidateContentForPages(int top, int page)
        {
            if (top * (page - 1) > this.GetAllTraders().Count())
                throw new Exception($"There is no page with number {page}");
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
        private void ValidateTraderInfo(TraderEntity info)
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
        private void ValidateTradersExistence(int id)
        {
            if (this.traderTableRepository.GetById(id) == null) throw new Exception("There is no user with given Id");
        }
        private void ValidateTradersExistence(TraderEntity traderEntity)
        {
            if (traderEntity == null)
            {
                throw new Exception("There is no user with given info");
            }
        }
    }
}
