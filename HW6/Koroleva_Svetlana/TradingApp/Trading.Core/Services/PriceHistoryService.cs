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
       

        public PriceHistoryService(ITableRepository<PriceHistory> tableRepository)
        {
            this.tableRepository = tableRepository;
           
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

     

        public PriceHistory GetEntityByID(int priceHistoryId)
        {
          
            if (!this.tableRepository.ContainsByPK(priceHistoryId))
            {
                throw new ArgumentException("PriceHistory doesn't exist");
            }
            return (PriceHistory)this.tableRepository.FindByPK(priceHistoryId);
        }


        public IEnumerable<PriceHistory> GetEntityByStockID(int pstockId)
        {
               return (IEnumerable<PriceHistory>)this.tableRepository.FindEntitiesByRequest(pstockId);
        }


        public decimal GetStockPriceByDateTime(PriceArguments args)
        {
            IEnumerable < PriceHistory > priceInfos = (IEnumerable<PriceHistory>) this.tableRepository.FindEntitiesByRequestDTO(args).ToList();
            PriceHistory priceinfo = priceInfos.Single();
            decimal price =priceinfo.Price;
            return price;


        }

        //!!
        public void EditPriceDateEnd(int stockId, DateTime dateTimeEndToSet)
        {
            //int[] param = { stockId };
            var phistories =(IEnumerable<PriceHistory>) this.tableRepository.FindEntitiesByRequest(stockId);
            //var p2 = GetEntityByStockID(stockId);
            var last= phistories.OrderByDescending(p => p.PriceHistoryID).First();
            last.DateTimeEnd = dateTimeEndToSet;
            tableRepository.SaveChanges();
        }

        public void SimulatePriceChange(int stockId, decimal priceBeforeChanges, DateTime dateTimeX)
        {
              PriceArguments arguments = new PriceArguments()
            {
                DateTimeLookUp = dateTimeX,
                StockId = stockId

            };
           
            Random random = new Random();
            bool isPriceIncreaseExpectation = false;
            if (random.Next(100) >50)
            {
                isPriceIncreaseExpectation = true;
            }
            double percent = (double)(random.Next(5))/100;
        
            DateTime dateTimeEnd=DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
            decimal price;
            EditPriceDateEnd(stockId, dateTimeX);
          
         
             
            if (isPriceIncreaseExpectation)
            {
                price = priceBeforeChanges * (decimal)(1 + percent);
            }
            else
            {
                price = priceBeforeChanges * (decimal)(1 - percent);
            }

            PriceInfo priceInfo = new PriceInfo
            {StockId=stockId,
                DateTimeBegin = dateTimeX.AddSeconds(1),
                DateTimeEnd = dateTimeEnd,
                Price = price };

            AddPriceInfo(priceInfo);
        }


    }
}
