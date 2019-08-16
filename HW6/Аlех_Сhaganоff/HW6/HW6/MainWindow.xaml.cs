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
using System.Windows.Threading;
using log4net;
using log4net.Config;

namespace HW6
{
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;
        public Program program;


        public MainWindow()
        {
            Logger.InitLogger();

            ISimulation simulation = new Simulation();
            IOutputProvider outputProvider = new OutputToStatusTextBox();
            IContextProvider contextProvider = new EntityFrameworkContextProvider();

            program = new Program(simulation, outputProvider, contextProvider);
            
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

            program.contextProvider.Traders.Load();
            program.contextProvider.Portfolios.Load();
            program.contextProvider.Shares.Load();
            program.contextProvider.Transactions.Load();

            traderViewSource.Source = program.contextProvider.Traders.Local;
            portfolioViewSource.Source = program.contextProvider.Portfolios.Local;
            shareViewSource.Source = program.contextProvider.Shares.Local;
            transactionViewSource.Source = program.contextProvider.Transactions.Local;
        }   

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.program.dataInteraction.Dispose();
        }

        private void SetVisibilityToCollapsed()
        {
            StatusTextBox.Visibility = Visibility.Collapsed;
            TraderInfoMainGrid.Visibility = Visibility.Collapsed;
            PortfoliosMainGrid.Visibility = Visibility.Collapsed;
            SharesMainGrid.Visibility = Visibility.Collapsed;
            TransacionsMainGrid.Visibility = Visibility.Collapsed;
        }

        private void traderViewSourceFilter(object sender, FilterEventArgs e)
        {
            var trader = e.Item as Trader;

            if (trader != null)
            {
                if (TraderFilterComboBox.SelectedIndex == 0)
                {
                    e.Accepted = true;
                }
                else if (TraderFilterComboBox.SelectedIndex == 1 && trader.Balance == 0)
                {
                    e.Accepted = true;
                }
                else if (TraderFilterComboBox.SelectedIndex == 2 && trader.Balance < 0)
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        private void UpdateView()
        {
            System.Windows.Data.CollectionViewSource traderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traderViewSource")));
            System.Windows.Data.CollectionViewSource portfolioViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("portfolioViewSource")));
            System.Windows.Data.CollectionViewSource shareViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("shareViewSource")));
            System.Windows.Data.CollectionViewSource transactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("transactionViewSource")));

            program.contextProvider.Traders.Load();
            program.contextProvider.Portfolios.Load();
            program.contextProvider.Shares.Load();
            program.contextProvider.Transactions.Load();

            traderViewSource.Source = program.contextProvider.Traders.Local;
            portfolioViewSource.Source = program.contextProvider.Portfolios.Local;
            shareViewSource.Source = program.contextProvider.Shares.Local;
            transactionViewSource.Source = program.contextProvider.Transactions.Local;

            ((CollectionViewSource)this.Resources["traderViewSource"]).View.Refresh();
            ((CollectionViewSource)this.Resources["portfolioViewSource"]).View.Refresh();
            ((CollectionViewSource)this.Resources["shareViewSource"]).View.Refresh();
            ((CollectionViewSource)this.Resources["transactionViewSource"]).View.Refresh();
        }

        public void RunTradingSimulation()
        {
            Task t = Task.Run(() =>
            {
                while (program.SimulationIsWorking)
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        program.simulation.PerformRandomOperation(program.dataInteraction, program.outputProvider);
                        UpdateView();
                    }));

                    for (int j = 1; j < 100 && program.SimulationIsWorking; j++)
                    {
                        if (program.SimulationIsWorking)
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
    }
}
