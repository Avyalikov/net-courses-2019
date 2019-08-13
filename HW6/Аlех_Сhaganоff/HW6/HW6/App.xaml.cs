using HW6.Classes;
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
        static public TradingContext context = new TradingContext();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Randomizer r = new Randomizer();
            
            //using (TradingContext context = new TradingContext())
            //{
            //Task.Run(() =>
            //{
            //    //program.Run();
            // r.Randomize(context);

            //context.SaveChanges();

            //    Console.WriteLine("Task completed");
            //});

            //var a = context.Traders.Single(t => t.TraderId == 1);
            //a.Balance = 777;

            //var check = context.SaveChanges();
            //Console.WriteLine("Saved " + check);
            //}

            //context.SaveChanges();


            Console.WriteLine("Main");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("Window_Closing");
            context.Dispose();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Window_Closed");
            context.Dispose();
        }
    }
}
