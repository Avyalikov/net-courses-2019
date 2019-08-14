using HW6.Classes;
using HW6.DataModel;
using HW6.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
        public Program program;
        private TradingContext context = new TradingContext();

        public MainWindow()
        {
            ISimulation simulation = new Simulation();
            IOutputProvider outputProvider = new OutputToStatusTextBox();

            program = new Program(simulation, outputProvider);
            
            InitializeComponent();
            SetVisibilityToCollapsed();
            StatusTextBox.Visibility = Visibility.Visible;
            
            AppWindow = this;
        }

        private void ProgramWindow_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource traderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traderViewSource")));
            System.Windows.Data.CollectionViewSource portfolioViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("portfolioViewSource")));
            System.Windows.Data.CollectionViewSource shareViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("shareViewSource")));
            System.Windows.Data.CollectionViewSource transactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("transactionViewSource")));

            context.Traders.Load();
            context.Portfolios.Load();
            context.Shares.Load();
            context.Transactions.Load();

            traderViewSource.Source = context.Traders.Local;
            portfolioViewSource.Source = context.Portfolios.Local;
            shareViewSource.Source = context.Shares.Local;
            transactionViewSource.Source = context.Transactions.Local;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.context.Dispose();
        }

        private void SetVisibilityToCollapsed()
        {
            StatusTextBox.Visibility = Visibility.Collapsed;
            TraderInfoMainGrid.Visibility = Visibility.Collapsed;
            PortfoliosMainGrid.Visibility = Visibility.Collapsed;
            SharesMainGrid.Visibility = Visibility.Collapsed;
            TransacionsMainGrid.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (program.SimulationIsWorking==false)
            {
                program.outputProvider.WriteLine("Simulation started");
                program.SimulationIsWorking = true;
                SimulationButton.Content = "Stop simulation";

                program.simulation.TradingSimulation(context, program, program.outputProvider);  
            }
            else
            {
                program.outputProvider.WriteLine("Simulation ended");
                program.SimulationIsWorking = false;
                SimulationButton.Content = "Start simulation";
            }
        }

        private void StatusButtonClick(object sender, RoutedEventArgs e)
        {
            SetVisibilityToCollapsed();
            StatusTextBox.Visibility = Visibility.Visible;
        }

        private void TraderInformationButton_Click(object sender, RoutedEventArgs e)
        {
            SetVisibilityToCollapsed();
            TraderInfoMainGrid.Visibility = Visibility.Visible;
        }

        private void PortfolioInformationButton_Click(object sender, RoutedEventArgs e)
        {
            SetVisibilityToCollapsed();
            PortfoliosMainGrid.Visibility = Visibility.Visible;
        }

        private void ShareInformationButton_Click(object sender, RoutedEventArgs e)
        {
            SetVisibilityToCollapsed();
            SharesMainGrid.Visibility = Visibility.Visible;
        }

        private void TransactionInformationButton_Click(object sender, RoutedEventArgs e)
        {
            SetVisibilityToCollapsed();
            TransacionsMainGrid.Visibility = Visibility.Visible;
        }



        private void PortfolioDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int traderId = (portfolioDataGrid.SelectedItem as Portfolio).TraderID;
            int shareId = (portfolioDataGrid.SelectedItem as Portfolio).ShareId;
            var portfolio = context.Portfolios.Where(p => p.TraderID == traderId && p.ShareId == shareId).SingleOrDefault();
            context.Portfolios.Remove(portfolio);
            context.SaveChanges();           
        }
    }
}
