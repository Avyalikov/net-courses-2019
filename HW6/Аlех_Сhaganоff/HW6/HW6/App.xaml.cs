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

            //using (TradingContext context = new TradingContext())
            //{
            //Task.Run(() =>
            //{
            //    program.Run();

            //    Console.WriteLine("Task completed");
            //});

            //context.SaveChanges();
            //}

            context.SaveChanges();
            context.Dispose();

            Console.WriteLine("Main");
        }
    }
}
