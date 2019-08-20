// <copyright file="PriceModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Modifiers
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
        private ITableRepository tableRepository;

        public PriceHistoryService(ITableRepository tableRepository)
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

            tableRepository.Add(priceHistory);
            tableRepository.SaveChanges();            
        }

     

        public PriceHistory GetEntityByID(int priceHistoryId)
        {
            if (this.tableRepository.ContainsByID(priceHistoryId))
            {
                throw new ArgumentException("PriceHistory doesn't exist");
            }
            return (PriceHistory)tableRepository.GetEntityByID(priceHistoryId);
        }


    
     /*   public decimal GetPriceByDateTime(DateTime dateTime, int stockId)
        {
            var priceInfo = db.PriceHistories.Where(ph => ph.DateTimeBegin <= dateTime && ph.DateTimeEnd >= dateTime).Select(p => p).Where(o => o.StockID == stockId).Select(p => p.Price).Single();
            return priceInfo;
        }
        */
        
          /*  public decimal GetTransactionPrice(TransactionHistory transaction)
        {
            int id = transaction.SalerOrderID;
            Order saleOrder = orderModifier.GetOrder(id);
            var priceInfo = db.PriceHistories.Where(ph => ph.DateTimeBegin <= transaction.DateTime && ph.DateTimeEnd >= transaction.DateTime).Select(p => p).Where(o => o.StockID == saleOrder.StockID).Select(p => p.Price).Single();
            return priceInfo;
        }*/

        
        /*   public void EditPrice()
        {
 Random random = new Random();
            bool isPriceIncreaseExpectation = false;
            if (random.Next(100) >50)
            {
                isPriceIncreaseExpectation = true;
            }
            double percent = (double)(random.Next(5))/100;
            DateTime dateTimeEnd=DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
            decimal price;
            db.PriceHistories.Where(pr=>pr.StockID==stockId).OrderByDescending(p => p.PriceHistoryID).First().DateTimeEnd = datetimeX;
             
            if (isPriceIncreaseExpectation)
            {
                price = currentPrice * (decimal)(1 + percent);
            }
            else
            {
                price = currentPrice * (decimal)(1 - percent);
            }
            PriceHistory priceHistory = new PriceHistory { StockID = stockId, DateTimeBegin = datetimeX.AddSeconds(1), DateTimeEnd = dateTimeEnd, Price = price };
            db.PriceHistories.Add(priceHistory);
            db.SaveChanges();
        }*/
    }
}
