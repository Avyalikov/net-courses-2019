using HW6.DataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HW6
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (TradingContext context = new TradingContext())
            {
                //Trader trader1 = new Trader();
                //trader1.FirstName = "A1";
                //trader1.LastName = "B1";
                //trader1.PhoneNumber = "123";

                //context.Traders.Add(trader1);

                //Trader trader2 = new Trader();
                //trader2.FirstName = "A2";
                //trader2.LastName = "B2";
                //trader2.PhoneNumber = "123";

                //context.Traders.Add(trader2);

                //Trader trader3 = new Trader();
                //trader3.FirstName = "A3";
                //trader3.LastName = "B3";
                //trader3.PhoneNumber = "123";

                //context.Traders.Add(trader3);
                

                context.SaveChanges();
            }
        }

    }
}
