namespace TradingApp.Core.LoggingServices
{
    using System;
    using System.Collections.Generic;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    public class LoggingShareService : ShareService
    {
        public LoggingShareService(IRepository<ShareEntity> shareTableRepository, IRepository<ShareTypeEntity> shareTypeTableRepository) 
            : base(shareTableRepository, shareTypeTableRepository) { }
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
    }
}
