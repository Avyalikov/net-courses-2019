namespace TradingApp.Core.ProxyForServices
{
    using System;
    using TradingApp.Core.Logger;
    using System.Collections.Generic;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;
    using TradingApp.Core.ServicesInterfaces;

    public class ShareProxy : IShareServices
    {
        private readonly ShareServices shareServices;

        public ShareProxy(ShareServices shareServices)
        {
            this.shareServices = shareServices;
        }

        public int AddNewShare(ShareInfo args)
        {
            try
            {
                int id = this.shareServices.AddNewShare(args);
                Logger.Log.Info($"Добавлена новая акция {args.Name}");
                return id;
            }
            catch (ArgumentException ex)
            {
                Logger.Log.Error(ex.Message);
                return 0;
            }
        }

        public void ChangeSharePrice(int id, decimal newPrice)
        {
            try
            {
                this.shareServices.ChangeSharePrice(id, newPrice);
                Logger.Log.Info($"У акции с id {id} новая цена {newPrice}");
            }
            catch (ArgumentException ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public ICollection<ShareEntity> GetAllShares()
        {
            return this.shareServices.GetAllShares();
        }

        public ShareEntity GetShareById(int id)
        {
            try
            {
                return this.shareServices.GetShareById(id);
            }
            catch (ArgumentException ex)
            {
                Logger.Log.Error(ex.Message);
                return null;
            }
        }

            public ICollection<ShareEntity> GetUsersShares(int userId)
            {
                return this.shareServices.GetUsersShares(userId);
            }

            public void Remove(int id)
            {
                try
                {
                    this.shareServices.Remove(id);
                    Logger.Log.Info($"Акция с id {id} удалена");
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message);
                }
            }

            public void Update(int id, ShareInfo args)
            {
                try
                {
                    this.shareServices.Update(id, args);
                    Logger.Log.Info($"Акция с id {id} изменена");
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message);
                }
            }
        }
    }
