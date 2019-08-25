using log4net;
using log4net.Config;
using System;
using System.Linq;
using System.Timers;

namespace Trading.DataModel
{
    internal class TradingCore : ITradingCore
    {

        private readonly IInputOutputDevice inputOutputDevice;
        private readonly IPhraseProvider phraseProvider;
        private readonly ITransactionHistoryRecorder transactionHistoryRecorder;
        private readonly ITransactionGenerator transactionGenerator;
        private readonly Settings settings;
        private static Timer aTimer;

        public TradingCore(
            IInputOutputDevice inputOutputDevice,
            IPhraseProvider phraseProvider,
            ITransactionHistoryRecorder transactionHistoryRecorder,
            ITransactionGenerator transactionGenerator,
            Settings settings)
        {
            this.inputOutputDevice = inputOutputDevice;
            this.phraseProvider = phraseProvider;
            this.transactionHistoryRecorder = transactionHistoryRecorder;
            this.transactionGenerator = transactionGenerator;
            this.settings = settings;
        }

        private bool Transaction(int sellerId, int buyerId, int sharesId, int quantity, out DateTime TransactionDateTime)
        {
            using (TradingDBContext db = new TradingDBContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Client seller = db.Clients.Where(c => c.ClientID == sellerId).First();
                        Client buyer = db.Clients.Where(c => c.ClientID == buyerId).First();
                        string sellerInfo = $"Seller: {seller.Name}, Balance: {seller.Balance}";
                        string buyerInfo = $"Buyer: {buyer.Name}, Balance: {buyer.Balance}";
                        Logger.Log.Info(sellerInfo);
                        Logger.Log.Info(buyerInfo);

                        if (buyer.Zone == "Black")
                        {
                            throw new SystemException(phraseProvider.GetPhrase("BuyerInBlackZone"));
                        }

                        decimal sum = db.Shares.Where(s => s.SharesID == sharesId).First().Price * quantity;
                        seller.Balance += sum;
                        if (seller.Balance == 0M)
                        {
                            seller.Zone = "Orange";
                        }

                        if (seller.Balance > 0)
                        {
                            seller.Zone = "Free";
                        }

                        buyer.Balance -= sum;
                        if (buyer.Balance == 0M)
                        {
                            buyer.Zone = "Orange";
                        }

                        if (buyer.Balance < 0)
                        {
                            buyer.Zone = "Black";
                        }

                        ClientShares sellersItem = db.ClientShares.Where(
                            s => s.Shares.SharesID == sharesId).Where(
                            s => s.ClientID == sellerId).Single();
                        sellersItem.Quantity -= quantity;

                        ClientShares buyersItem = db.ClientShares.Where(
                            s => s.Shares.SharesID == sharesId).Where(
                            s => s.ClientID == buyerId).Single();
                        buyersItem.Quantity += quantity;
                        string sharesInfo = $"Shares type: {sellersItem.Shares.SharesType}, Quantity: {quantity}, Total: {sum}";
                        Logger.Log.Info(sharesInfo);

                        db.SaveChanges();
                        transaction.Commit();
                        this.inputOutputDevice.Print(this.phraseProvider.GetPhrase("Success"));
                        Logger.Log.Info(phraseProvider.GetPhrase("Success"));
                        sellerInfo = $"Seller: {seller.Name}, Balance: {seller.Balance}";
                        buyerInfo = $"Buyer: {buyer.Name}, Balance: {buyer.Balance}";
                        Logger.Log.Info(sellerInfo);
                        Logger.Log.Info(buyerInfo);
                      TransactionDateTime = DateTime.Now;
                        return true;
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException)
                    {
                        transaction.Rollback();
                        this.inputOutputDevice.Print($"{this.phraseProvider.GetPhrase("Failure")}: " +
                            $"{this.phraseProvider.GetPhrase("Validation")}");
                        Logger.Log.Info($"{this.phraseProvider.GetPhrase("Failure")}: " +
                            $"{this.phraseProvider.GetPhrase("Validation")}");
                        TransactionDateTime = DateTime.Now;
                        return false;
                    }
                    catch (SystemException ex)
                    {
                        transaction.Rollback();
                        this.inputOutputDevice.Print($"{this.phraseProvider.GetPhrase("Failure")}: {ex.Message}");
                        Logger.Log.Info($"{this.phraseProvider.GetPhrase("Failure")}: {ex.Message}");
                        TransactionDateTime = DateTime.Now;
                        return false;
                    }
                }
            }
        }
        public void Run()
        {
            inputOutputDevice.Print(phraseProvider.GetPhrase("Welcome"));
            SetTimer();
            Logger.InitLogger();

            char userinput;
            do
            {
                userinput = inputOutputDevice.KeyInput();
                if (userinput == settings.PauseTrades)
                {
                    if (aTimer.Enabled)
                    {
                        aTimer.Stop();
                    }
                    else
                    {
                        aTimer.Start();
                    }
                }
            } while (userinput != settings.ExitCode);
            aTimer.Dispose();
        }
        private void StartTrades(Object source, ElapsedEventArgs e)
        {
            int sellerId, buyerId, sharesId, quantity;
            inputOutputDevice.Print(phraseProvider.GetPhrase("GeneratingTransaction"));
            transactionGenerator.GenerateTransactionParams(out sellerId, out buyerId, out sharesId, out quantity);
            inputOutputDevice.Print(phraseProvider.GetPhrase("StartingTransaction"));
            Logger.Log.Info(phraseProvider.GetPhrase("StartingTransaction"));
            DateTime TransactionDateTime;
            if (Transaction(sellerId, buyerId, sharesId, quantity, out TransactionDateTime))
            {
                var transaction = transactionHistoryRecorder.RecordTransactionHistory(sellerId, buyerId, sharesId, quantity, TransactionDateTime);
                inputOutputDevice.Print(phraseProvider.GetPhrase("TransactionDetails"));
                inputOutputDevice.Print($"{transaction.Seller.Name} {transaction.Buyer.Name} {transaction.SelledItem.SharesType} {transaction.Quantity}");
            }
        }
        private void SetTimer()
        {
            aTimer = new Timer(settings.TransactionsTimeout);
            aTimer.Elapsed += StartTrades;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
    }
}