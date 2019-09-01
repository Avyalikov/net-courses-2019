namespace TradingApp.Core.LoggingServices
{
    using System;
    using System.Collections.Generic;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    public class LoggingShareService : ShareService
    {
        public LoggingShareService(IRepository<ShareEntity> shareTableRepository,
            IRepository<ShareTypeEntity> shareTypeTableRepository,
            IRepository<StockEntity> stockTableRepository,
            IRepository<TraderEntity> traderTableRepository)
            : base(shareTableRepository, shareTypeTableRepository, stockTableRepository, traderTableRepository) { }
        public override void ChangeShareType(int shareId, int shareTypeId)
        {
            try
            {
                base.ChangeShareType(shareId, shareTypeId);
                Logger.FileLogger.Info($"Type of the share with Id = {shareId} was changed to the type with Id = {shareTypeId}");
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
        public override List<ShareEntity> GetAllSharesByTraderId(int traderId)
        {
            try
            {
                var allShares = base.GetAllSharesByTraderId(traderId);
                Logger.FileLogger.Info($"Request for all shares owned by trader with id = {traderId}");
                return allShares;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
        public override List<string> GetAllSharesListByTraderId(int traderId)
        {
            try
            {
                var allShares = base.GetAllSharesListByTraderId(traderId);
                Logger.FileLogger.Info($"Request for all shares list owned by trader with id = {traderId}");
                return allShares;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
        public override void AddNewShare(ShareInfo shareInfo)
        {
            try
            {
                base.AddNewShare(shareInfo);
                Logger.FileLogger.Info($"Added new share");
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
        public override List<ShareEntity> GetAllShares()
        {
            try
            {
                var allShares = base.GetAllShares();
                Logger.FileLogger.Info($"Request for all shares");
                return allShares;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
        public override void UpdateShare(ShareInfo shareInfo)
        {
            try
            {
                base.UpdateShare(shareInfo);
                Logger.FileLogger.Info($"Updated share with Id = {shareInfo.Id}");
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
        public override void RemoveShare(int shareId)
        {
            try
            {
                base.RemoveShare(shareId);
                Logger.FileLogger.Info($"Removed share with Id = {shareId}");
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
    }
}
