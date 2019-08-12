using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HW6.Classes
{
    public class Simulation
    {
        public async Task TradingSimulation(TradingContext context)
        {
            Task t = Task.Run(() =>
            {
                for (int i = 1; i <= 10 && MainWindow.SimulationIsWorking; i++)
                {
                    Console.WriteLine("AAA" + i);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MainWindow.AppWindow.ConsoleWindow.AppendText("AAA" + i + Environment.NewLine);
                    });

                    for (int j =1; j <250 && MainWindow.SimulationIsWorking; j++)
                    {
                        if(MainWindow.SimulationIsWorking)
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

        public void RandomOperation(TradingContext context)
        {
            int numberOfTraders = context.Traders.Count();
            int sellerId;
            int buyerId;
            int shareId;
            decimal sharePrice;
            int purchaseQuantity;
            
            try
            {
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

                    int counter = 0;

                    while (sellerId == buyerId && counter < 1000)
                    {
                        buyerId = new Random().Next(1, numberOfTraders + 1);
                        counter++;
                    }
                }
                else
                {
                    throw new Exception("Not enough traders for a transaction");
                }

                List<int> availableShares = context.Portfolios.Where(p => p.TraderID == sellerId).Select(x => x.ShareId).ToList();

                shareId = new Random().Next(0, availableShares.Count);

                sharePrice = context.Shares.Where(s => s.ShareId == shareId).Select(x => x.Price).FirstOrDefault();
                
                purchaseQuantity = new Random().Next(1, context.Portfolios.Where(p=>p.TraderID == sellerId && p.ShareId == shareId).Select(x=>x.Quantity).FirstOrDefault()+1);
            }
            catch(Exception e)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainWindow.AppWindow.ConsoleWindow.AppendText(e.Message);
                });
            }
        }
    }
}
