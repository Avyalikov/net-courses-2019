using HW6.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;
        public static bool SimulationIsWorking = false;
        public MainWindow()
        {
            InitializeComponent();

            AppWindow = this;
        }

        CancellationTokenSource source = new CancellationTokenSource();
        Simulation simulation = new Simulation();
        CancellationToken token;
        

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SimulationIsWorking==false)
            {
                System.Console.WriteLine("Begin simulation");
                SimulationIsWorking = true;
                MainWindow.AppWindow.SimulationButton.Content = "Stop simulation";

                await simulation.TradingSimulation(Program.context);  
            }
            else
            {
                System.Console.WriteLine("End simulation");
                SimulationIsWorking = false;
                MainWindow.AppWindow.SimulationButton.Content = "Start simulation";
            }
            //else
            //{
            //    //System.Console.WriteLine("Here");
            //    //SimulationIsWorking = false;

            //    //if (source != null)
            //    //{
            //    //    source.Cancel();
            //    //}
            //}


        }
    }
}
