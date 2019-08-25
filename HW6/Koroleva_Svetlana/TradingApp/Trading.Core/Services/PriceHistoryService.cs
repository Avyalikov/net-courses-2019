// <copyright file="PriceModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Services
{
  
    using Trading.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Repositories;
    using Trading.Core.DTO;
   
    /// <summary>
    /// PriceModifier description
    /// </summary>
    public class PriceHistoryService 
    {
        private ITableRepository<PriceHistory> tableRepository;
        private IDTOMethodsforPriceHistory dtoMethods;
       
        public PriceHistoryService(ITableRepository<PriceHistory> tableRepository, IDTOMethodsforPriceHistory dTOMethodsforPriceHistory)
        {
            this.tableRepository = tableRepository;
            this.dtoMethods = dTOMethodsforPriceHistory;
        }

        public void AddPriceInfo(PriceInfo args)
        {
            PriceHistory priceHistory = new PriceHistory()
            {
                StockID = args.StockId,
                DateTimeBegin = args.DateTimeBegin,
                DateTimeEnd = args.DateTimeEnd,
                Price = args.Price
            };
            if (this.tableRepository.ContainsDTO(priceHistory))
            {
                throw new ArgumentException("This price history exists. Can't continue");
            };
            tableRepository.Add(priceHistory);
            tableRepository.SaveChanges();            
        }

        public decimal GetStockPriceByDateTime(PriceArguments args)
        {
            IEnumerable < PriceHistory > priceInfos = this.dtoMethods.FindEntitiesByRequestDTO(args).ToList();
            PriceHistory priceinfo = priceInfos.Single();
            decimal price =priceinfo.Price;
            return price;
        }

        //!!
        public void EditPriceDateEnd(int stockId, DateTime dateTimeEndToSet)
        {
            var phistories =this.dtoMethods.FindEntitiesByRequest(stockId);
            var last= phistories.OrderByDescending(p => p.PriceHistoryID).First();
            last.DateTimeEnd = dateTimeEndToSet;
            tableRepository.SaveChanges();
        }

    }
}
