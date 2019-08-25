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
using log4net;
using log4net.Config;

namespace HW6.Classes
{
    public class Simulation: ISimulation
    {
        public virtual (int sellerId, int buyerId, int shareId, decimal sharePrice, int purchaseQuantity)
        PerformRandomOperation(IDataInteraction dataProvider, IOutputProvider outputProvider, ILogger logger)
        {
            int sellerId;
            int buyerId;
            int shareId;
            decimal sharePrice;
            int purchaseQuantity;
            
            try
            {
                int numberOfTraders = dataProvider.GetNumberOfTraders();

                if (numberOfTraders > 1)
                {
                    List<int> availableSellers = dataProvider.GetAvailableSellers();

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

                List<int> availableShares = dataProvider.GetAvailableShares(sellerId);
#if DEBUG
                outputProvider.WriteLine("Available shares types = " + availableShares.Count);
#endif
                logger.Write("Available shares types = " + availableShares.Count);

                shareId = availableShares[new Random().Next(0, availableShares.Count)];
                sharePrice = dataProvider.GetSharePrice(shareId);
                purchaseQuantity = new Random().Next(1, dataProvider.GetShareQuantityFromPortfoio(sellerId, shareId) + 1);

                return (sellerId, buyerId, shareId, sharePrice, purchaseQuantity);

                //UpdateDatabase(dataProvider, outputProvider, logger, sellerId, buyerId, shareId, sharePrice, purchaseQuantity);
            }
            catch(Exception e)
            {
                outputProvider.WriteLine(e.Message);
                logger.Write(e.Message);
            }

            return (0, 0, 0, 0M, 0);
        }

        public void UpdateDatabase(IDataInteraction dataProvider, IOutputProvider outputProvider, ILogger logger, int sellerId, int buyerId, int shareId, decimal sharePrice, int purchaseQuantity)
        {
            try
            {
                if (sellerId == 0 || buyerId == 0 || shareId == 0 || sharePrice == 0 || purchaseQuantity == 0)
                {
                    logger.Write("Incorrect data from randomizer");
                    throw new Exception("Incorrect data from randomizer");
                }
                
                var sellerToChange = dataProvider.GetTrader(sellerId);

                if (sellerToChange != null)
                {
                    sellerToChange.Balance += sharePrice * purchaseQuantity;
                }

                var buyerToChange = dataProvider.GetTrader(buyerId);

                if (buyerToChange != null)
                {
                    buyerToChange.Balance -= sharePrice * purchaseQuantity;
                }

                var sellerShareRecordToChange = dataProvider.GetPortfolio(sellerId, shareId);

                if (sellerShareRecordToChange != null)
                {
                    sellerShareRecordToChange.Quantity -= purchaseQuantity;

                    if (sellerShareRecordToChange.Quantity == 0)
                    {
#if DEBUG
                        outputProvider.WriteLine("Removed share record with 0 quantity");

#endif
                        logger.Write("Removed share record with 0 quantity");

                        dataProvider.RemovePortfolio(sellerShareRecordToChange);
                    }
                }

                if(dataProvider.GetPortfoliosCount(buyerId, shareId)>0)
                {
                    var buyerShareRecordToChange = dataProvider.GetPortfolio(buyerId, shareId);

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
                    logger.Write("Add new record to portfolio");

                    dataProvider.AddPortfolio(buyerId, shareId, purchaseQuantity);
                }

                var transaction = dataProvider.AddTransaction(buyerId, sellerId, shareId, sharePrice, purchaseQuantity);

                dataProvider.SaveChanges();

                string message = "Buyer = " + transaction.BuyerId + " Seller = " + transaction.SellerId + " Share name = " + transaction.ShareId +
                     " Quantity = " + transaction.Quantity + " Price per share = " + transaction.PricePerShare +
                     " Transaction total = " + transaction.PricePerShare * transaction.Quantity + " Timestamp = " + transaction.DateTime;

                outputProvider.WriteLine(message);
                logger.Write(message);
            }
            catch(Exception e)
            {
                outputProvider.WriteLine(e.Message);
                logger.Write(e.Message);
            }           
        }
    }
}
