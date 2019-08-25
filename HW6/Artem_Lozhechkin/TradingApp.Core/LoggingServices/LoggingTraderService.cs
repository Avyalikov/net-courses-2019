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
    }
}
