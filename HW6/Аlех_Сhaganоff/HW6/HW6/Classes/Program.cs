using HW6.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW6.Classes
{
    public class Randomizer
    {
        public async Task Randomize(TradingContext context)
        {
           Random bal = new Random();
           Random share = new Random();
           Random shareTypes = new Random();
           Random quantity = new Random();

           for (int i = 1; i <= 90; i++)
           {
                var result = context.Traders.SingleOrDefault(t => t.TraderId == i);

                if (result != null)
                {
                   result.Balance = bal.Next(1000, 10000);
                }
           }

           for (int i = 1; i <= 90; i++)
           {
               var result = context.Traders.SingleOrDefault(t => t.TraderId == i);
               if (result != null)
               {
                   for (int j = 1; j <= shareTypes.Next(1, 11); j++)
                   {
                       int newShareId = share.Next(1, 51);

                       var c = context.Portfolios.Where(p => p.TraderID == i && p.ShareId == newShareId).ToList();
                       int check = c.Count();

                       if (check == 0)
                       {
                           result.Portfolio.Add(new Portfolio { TraderID = i, ShareId = newShareId, Quantity = quantity.Next(1, 11) });
                       }
                   }
               }
           }

           try
           {
                context.SaveChanges();
           }
           catch(Exception e)
           {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainWindow.AppWindow.ConsoleWindow.AppendText("Randomization failed");
                    MainWindow.AppWindow.ConsoleWindow.AppendText(e.Message);
                });
           }          
        }
    }
}
