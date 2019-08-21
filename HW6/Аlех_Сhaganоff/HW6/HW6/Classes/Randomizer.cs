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
    public class Randomizer
    {
        public void Randomize(IContextProvider context, int numberOfTraders, int numberOfShareTypes)
        {
           Random bal = new Random();
           Random share = new Random();
           Random shareTypes = new Random();
           Random quantity = new Random();

           for (int i = 1; i <= numberOfTraders; i++)
           {
                var result = context.Traders.First(t => t.TraderId == i);

                if (result != null)
                {
                   result.Balance = bal.Next(1000, 10000);
                }
           }

           for (int i = 1; i <= numberOfTraders; i++)
           {
               var result = context.Traders.First(t => t.TraderId == i);

               if (result != null)
               {
                   List<int> shareTypeList = new List<int>(); 

                   for (int j = 1; j <= shareTypes.Next(1, 11); j++)
                   {
                       int newShareId = share.Next(1, numberOfShareTypes+1);
                       
                       if (!shareTypeList.Contains(newShareId))
                       {
                           //result.Portfolio.Add(new Portfolio { TraderID = i, ShareId = newShareId, Quantity = quantity.Next(1, 11) });                        context.po
                           context.Portfolios.Add(new Portfolio { TraderID = i, ShareId = newShareId, Quantity = quantity.Next(1, 11) });
                           shareTypeList.Add(newShareId);
                       }
                   }
               }
           }

           try
           {
                var check = context.SaveChanges();
                Console.WriteLine("Saved: " + check);
           }
           catch(Exception e)
           {
                System.Console.WriteLine("Randomization failed");
                System.Console.WriteLine(e.Message);
           }          
        }
    }
}
