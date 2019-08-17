namespace TradingApp.Logic
{
    using System;
    using System.Linq;
    using TradingApp.Data;
    using TradingApp.Data.Models;

    class ShareLogic
    {
        private readonly IAppDbContext dbProvider;

        public ShareLogic(IAppDbContext dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        public IQueryable<Share> ListStocks()
        {          
            return dbProvider.Share;
        }

        public string ChangeStockPrice(int id, decimal newPrice)
        {
            var share = dbProvider.Share.Where(s => s.Id == id).FirstOrDefault();
            if (share == null)
                throw new Exception("ДАННАЯ АКЦИЯ НЕ НАЙДЕНА");
            decimal oldPrice = share.Price;
            share.Price = newPrice;
            dbProvider.SaveChanges();
            return $"ИЗМЕНЕНИЕ ЦЕНЫ: Акция {share.Name} имеет новую цену {share.Price} вместо {oldPrice}";
        }       
    }
}
