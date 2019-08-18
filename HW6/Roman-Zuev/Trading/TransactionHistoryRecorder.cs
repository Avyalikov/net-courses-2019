using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.DataModel
{
    class DBTransactionHistoryRecorder: ITransactionHistoryRecorder
    {
        IPhraseProvider phraseProvider;
        IInputOutputDevice inputOutputDevice;
        public DBTransactionHistoryRecorder (IPhraseProvider phraseProvider, IInputOutputDevice inputOutputDevice)
        {
            this.inputOutputDevice = inputOutputDevice;
            this.phraseProvider = phraseProvider;
        }
        public TransactionHistory RecordTransactionHistory(int sellerId, int buyerId, int sharesId, int quantity, DateTime dateTime)
        {
            using (TradingDBContext db = new TradingDBContext())
            {
                try
                {
                    Client seller = db.Clients.Where(c => c.ClientID == sellerId).Single();
                    Client buyer = db.Clients.Where(c => c.ClientID == buyerId).Single();
                    Shares shares = db.Shares.Where(s => s.SharesID == sharesId).Single();
                    TransactionHistory newRecord = new TransactionHistory {
                        Seller = seller,
                        Buyer = buyer,
                        DateTime = dateTime,
                        Quantity = quantity,
                        SelledItem = shares
                    };

                    db.TransactionHistories.Add(newRecord);
                    db.SaveChanges();
                    return newRecord;
                }
                catch (SystemException ex)
                {
                    inputOutputDevice.Print($"{phraseProvider.GetPhrase("HistoryError")}: {ex.Message}");
                    return new TransactionHistory();
                }
            }
        }
    }
}
