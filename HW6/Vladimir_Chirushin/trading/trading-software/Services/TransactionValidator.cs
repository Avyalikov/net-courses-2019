using System.Linq;

namespace trading_software
{
    public class TransactionValidator : ITransactionValidator
    {
        public bool Validate(Transaction transaction)
        {
            using (var db = new TradingContext())
            {
                bool IsSellerHasThisStocks =
                    db.BlockOfSharesTable.Where(s=>s.ClienInBLock.ClientID == transaction.Seller.ClientID && 
                                              s.StockInBlock.StockID == transaction.Stocks.StockID).Any();
                bool IsSellerAndBuyerDifferent =
                    transaction.Seller.ClientID != transaction.Buyer.ClientID;

                bool IsBuyerCanAffordStocks = 
                    db.Clients.Where(c=>c.ClientID == transaction.Buyer.ClientID)
                    .FirstOrDefault().Balance > (transaction.Stocks.Price*transaction.Amount);
                if (IsSellerAndBuyerDifferent && 
                    IsSellerHasThisStocks && 
                    IsBuyerCanAffordStocks)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
