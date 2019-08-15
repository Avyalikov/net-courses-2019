using HW6.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace HW6.Classes
{
    public class Simulation: ISimulation
    {
        public void TradingSimulation(TradingContext context, Program program, IOutputProvider outputProvider)
        {
            Task t = Task.Run(() =>
            {
                while (program.SimulationIsWorking)
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => 
                    {
                        RandomOperation(context, outputProvider);
                        updateView(context);
                    }));

                    for (int j =1; j <100 && program.SimulationIsWorking; j++)
                    {
                        if(program.SimulationIsWorking)
                        {
                            Thread.Sleep(10);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            });
        }

        private void RandomOperation(TradingContext context, IOutputProvider outputProvider)
        {
            int sellerId;
            int buyerId;
            int shareId;
            decimal sharePrice;
            int purchaseQuantity;
            
            try
            {
                int numberOfTraders = context.Traders.Count();

                if (numberOfTraders > 1)
                {
                    List<int> availableSellers = context.Portfolios.Where(p => p.Quantity > 0).Select(x => x.TraderID).Distinct().ToList();

                    if (availableSellers.Count > 0)
                    {
                        sellerId = availableSellers[new Random().Next(0, availableSellers.Count)];
                    }
                    else
                    {
                        throw new Exception("No traders with shares");
                    }

                    buyerId = new Random().Next(1, numberOfTraders + 1);

                    while (sellerId == buyerId)
                    {
                        buyerId = new Random().Next(1, numberOfTraders + 1);
                    }

                    if(buyerId == sellerId)
                    {
                        throw new Exception("buyerId == sellerId");
                    }
                }
                else
                {
                    throw new Exception("Not enough traders for a transaction");
                }

                List<int> availableShares = context.Portfolios.Where(p => p.TraderID == sellerId).Select(x => x.ShareId).ToList();

#if DEBUG
                outputProvider.WriteLine("Available shares types = " + availableShares.Count);
#endif

                shareId = availableShares[new Random().Next(0, availableShares.Count)];

                sharePrice = context.Shares.Where(s => s.ShareId == shareId).Select(x => x.Price).FirstOrDefault();
                
                purchaseQuantity = new Random().Next(1, context.Portfolios.Where(p=>p.TraderID == sellerId && p.ShareId == shareId).Select(x=>x.Quantity).FirstOrDefault()+1);

                UpdateDatabase(context, outputProvider, sellerId, buyerId, shareId, sharePrice, purchaseQuantity);
            }
            catch(Exception e)
            {
                outputProvider.WriteLine(e.Message);
            }
        }

        private void UpdateDatabase(TradingContext context, IOutputProvider outputProvider, int sellerId, int buyerId, int shareId, decimal sharePrice, int purchaseQuantity)
        {
            try
            {
                var sellerToChange = context.Traders.SingleOrDefault(t => t.TraderId == sellerId);
                if (sellerToChange != null)
                {
                    sellerToChange.Balance += sharePrice * purchaseQuantity;
                }

                var buyerToChange = context.Traders.SingleOrDefault(t => t.TraderId == buyerId);
                if (buyerToChange != null)
                {
                    buyerToChange.Balance -= sharePrice * purchaseQuantity;
                }

                var sellerShareRecordToChange = context.Portfolios.SingleOrDefault(p => p.TraderID == sellerId && p.ShareId == shareId);
                if (sellerShareRecordToChange != null)
                {
                    sellerShareRecordToChange.Quantity -= purchaseQuantity;

                    if (sellerShareRecordToChange.Quantity == 0)
                    {
#if DEBUG
                        outputProvider.WriteLine("Removed share record with 0 quantity");

#endif
                        context.Portfolios.Remove(sellerShareRecordToChange);
                    }
                }

                if (context.Portfolios.Where(p => p.TraderID == buyerId && p.ShareId == shareId).ToList().Count > 0)
                {
                    var buyerShareRecordToChange = context.Portfolios.SingleOrDefault(p => p.TraderID == buyerId && p.ShareId == shareId);

                    if (buyerShareRecordToChange != null)
                    {
                        buyerShareRecordToChange.Quantity += purchaseQuantity;
                    }
                }
                else
                {
#if DEBUG
                    outputProvider.WriteLine("Add new record to portfolio");

#endif
                    context.Portfolios.Add(new DataModel.Portfolio
                    {
                        TraderID = buyerId,
                        ShareId = shareId,
                        Quantity = purchaseQuantity
                    });
                }

                var transaction = context.Transactions.Add(new DataModel.Transaction
                {
                    BuyerId = buyerId,
                    SellerId = sellerId,
                    ShareId = shareId,
                    PricePerShare = sharePrice,
                    Quantity = purchaseQuantity,
                    DateTime = DateTime.Now
                });

                context.SaveChanges();

                outputProvider.WriteLine("Buyer = " + transaction.BuyerId + " Seller = " + transaction.SellerId + " Share name = " + transaction.ShareId +
                     " Quantity = " + transaction.Quantity + " Price per share = " + transaction.PricePerShare +
                     " Transaction total = " + transaction.PricePerShare * transaction.Quantity + " Timestamp = " + transaction.DateTime);
            }
            catch(Exception e)
            {
                outputProvider.WriteLine(e.Message);
            }           
        }

        private void updateView(TradingContext context)
        {
            System.Windows.Data.CollectionViewSource traderViewSource = ((System.Windows.Data.CollectionViewSource)(MainWindow.AppWindow.FindResource("traderViewSource")));
            System.Windows.Data.CollectionViewSource portfolioViewSource = ((System.Windows.Data.CollectionViewSource)(MainWindow.AppWindow.FindResource("portfolioViewSource")));
            //System.Windows.Data.CollectionViewSource shareViewSource = ((System.Windows.Data.CollectionViewSource)(MainWindow.AppWindow.FindResource("shareViewSource")));
            //System.Windows.Data.CollectionViewSource transactionViewSource = ((System.Windows.Data.CollectionViewSource)(MainWindow.AppWindow.FindResource("transactionViewSource")));

            context.Traders.Load();
            context.Portfolios.Load();
            //context.Shares.Load();
            //context.Transactions.Load();

            traderViewSource.Source = context.Traders.Local;
            portfolioViewSource.Source = context.Portfolios.Local;
            //shareViewSource.Source = context.Shares.Local;
            //transactionViewSource.Source = context.Transactions.Local;

            ((CollectionViewSource)MainWindow.AppWindow.Resources["traderViewSource"]).View.Refresh();
            ((CollectionViewSource)MainWindow.AppWindow.Resources["portfolioViewSource"]).View.Refresh();
            //((CollectionViewSource)MainWindow.AppWindow.Resources["shareViewSource"]).View.Refresh();
            //((CollectionViewSource)MainWindow.AppWindow.Resources["transactionViewSource"]).View.Refresh();
        }
    }
}
