namespace TradingApp.Core.LoggingServices
{
    using System;
    using System.Collections.Generic;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    public class LoggingTraderService : TraderService
    {
        public LoggingTraderService(IRepository<TraderEntity> traderTableRepository) : base(traderTableRepository)
        {
        }
        public override List<TraderEntity> GetAllTraders()
        {
            try
            {
                var allTraders = base.GetAllTraders();
                Logger.FileLogger.Info($"List of all traders was requested");
                return allTraders;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
        public override List<TraderEntity> GetTradersFromBlackZone()
        {
            try
            {
                var blackZoneTraders = base.GetTradersFromBlackZone();
                Logger.FileLogger.Info($"List of traders from black zone was requested");
                return blackZoneTraders;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
        public override List<TraderEntity> GetTradersFromOrangeZone()
        {
            try
            {
                var orangeZoneTraders = base.GetTradersFromOrangeZone();
                Logger.FileLogger.Info($"List of traders from orange zone was requested");
                return orangeZoneTraders;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }

        public override IEnumerable<string> GetUserLists(int top, int page)
        {
            try
            {
                var users = base.GetUserLists(top, page);
                Logger.FileLogger.Info($"Page {page} with {top} traders was requested");
                return users;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }

        public override string GetUserStatus(int id)
        {
            try
            {
                var status = base.GetUserStatus(id);
                Logger.FileLogger.Info($"Status of user with id = {id} was requested");
                return status;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }

        public override int RegisterNewUser(TraderInfo info)
        {
            try
            {
                var traderId = base.RegisterNewUser(info);
                Logger.FileLogger.Info("Registered new user");
                return traderId;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }

        public override void RemoveUser(TraderInfo info)
        {
            try
            {
                base.RemoveUser(info);
                Logger.FileLogger.Info($"Removed user {info.FirstName} {info.LastName}");
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }

        public override void UpdateUser(TraderEntity traderEntity)
        {
            try
            {
                base.UpdateUser(traderEntity);
                Logger.FileLogger.Info($"Removed user with id = {traderEntity.Id}");
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
    }
}
