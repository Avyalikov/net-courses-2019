using HW6.DataModel;
using HW6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW6.Classes
{
    public class Randomizer: IRandomizer
    {
        public void Randomize(TradingContext context, IOutputProvider outputProvider)
        {
           Random bal = new Random();
           Random share = new Random();
           Random shareTypes = new Random();
           Random quantity = new Random();

           for (int i = 1; i <= 90; i++)
           {
                var result = context.Traders.Single(t => t.TraderId == i);

                if (result != null)
                {
                   result.Balance = bal.Next(1000, 10000);
                }
           }

           for (int i = 1; i <= 90; i++)
           {
               var result = context.Traders.Single(t => t.TraderId == i);

               if (result != null)
               {
                   List<int> shareTypeList = new List<int>(); 

                   for (int j = 1; j <= shareTypes.Next(1, 11); j++)
                   {
                       int newShareId = share.Next(1, 51);
                       
                       if (!shareTypeList.Contains(newShareId))
                       {
                           result.Portfolio.Add(new Portfolio { TraderID = i, ShareId = newShareId, Quantity = quantity.Next(1, 11) });
                           shareTypeList.Add(newShareId);
                       }
                   }
               }
           }

           try
           {
                //var check = context.SaveChanges();
                //Console.WriteLine("Saved: " + check);

           }
           catch(Exception e)
           {
                outputProvider.WriteLine("Randomization failed");
                outputProvider.WriteLine(e.Message);
           }          
        }
    }
}
