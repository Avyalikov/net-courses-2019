// <copyright file="DTOMethodsforPriceHistory.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.DTO;
    using Trading.Core.Model;
    using TradingApp.DAL;

    /// <summary>
    /// DTOMethodsforPriceHistory description
    /// </summary>
    public class DTOMethodsforPriceHistory:IDTOMethodsforPriceHistory
   {
        ExchangeContext db;

        public DTOMethodsforPriceHistory(ExchangeContext db)
        {
            this.db = db;
        }

        public IEnumerable<PriceHistory> FindEntitiesByRequest(int stockId)
        {

            var pricehistory = this.db.PriceHistories
               .Select(p => p).Where(o => o.StockID == stockId);

            return pricehistory;
        }

        public IEnumerable<PriceHistory> FindEntitiesByRequestDTO(PriceArguments DTOarguments)
        {
            PriceArguments args = DTOarguments;

            var pricehist = this.db.PriceHistories.ToList();
            var pricehistory = this.db.PriceHistories
                .Where(ph => ph.DateTimeBegin <= args.DateTimeLookUp && ph.DateTimeEnd >= args.DateTimeLookUp && ph.StockID == args.StockId)
                .Select(p => p).ToList();

            return pricehistory;
        }
    }
}
