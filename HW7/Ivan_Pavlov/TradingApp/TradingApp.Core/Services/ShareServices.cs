namespace TradingApp.Core.Services
{
    using System;
    using System.Collections.Generic;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.ServicesInterfaces;

    public class ShareServices : IShareServices
    {
        private readonly IShareTableRepository repo;

        public ShareServices(IShareTableRepository repo)
        {
            this.repo = repo;
        }

        public ICollection<ShareEntity> GetAllShares()
        {
            return this.repo.GetAllShares();
        }

        public int AddNewShare(ShareInfo args)
        {
            var ShareToAdd = new ShareEntity()
            {
                Name = args.Name,
                CompanyName = args.CompanyName,
                Price = args.Price
            };

            if (this.repo.Contains(ShareToAdd))
            {
                throw new ArgumentException("Данная акция уже существвует");
            }

            repo.Add(ShareToAdd);

            repo.SaveChanges();

            return ShareToAdd.Id;
        }

        public void ChangeSharePrice(int id, decimal newPrice)
        {
            if (newPrice < 0)
            {
                throw new ArgumentException("Недопустимая цена акции");
            }
            var share = this.repo.GetShareById(id);
            if (share == null)
            {
                throw new ArgumentException("Неверная акция");
            }
            share.Price = newPrice;
            this.repo.SaveChanges();
        }

        public void Remove(int id)
        {
            repo.Remove(id);
            repo.SaveChanges();
        }

        public void Update(int id, ShareInfo args)
        {
            ShareEntity share = repo.GetShareById(id);
            share.Name = args.Name ?? share.Name;
            share.CompanyName = args.CompanyName ?? share.CompanyName;              
            repo.SaveChanges();
        }

        public ICollection<ShareEntity> GetUsersShares(int userId)
        {
            return repo.GetUsersShares(userId);
        }

        public ShareEntity GetShareById(int id)
        {
            var share = this.repo.GetShareById(id);
            if (share == null)
                throw new ArgumentException($"Акция с id { id } не существует");
            return share;
        }
    }
}
