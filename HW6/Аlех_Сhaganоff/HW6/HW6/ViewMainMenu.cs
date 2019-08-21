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
        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            if (program.SimulationIsWorking == false)
            {
                program.outputProvider.WriteLine("Simulation started");
                program.logger.Write("Simulation started");
                program.SimulationIsWorking = true;
                SimulationButton.Content = "Stop simulation";

                RunTradingSimulation();
            }
            else
            {
                program.outputProvider.WriteLine("Simulation ended");
                program.logger.Write("Simulation ended");
                program.SimulationIsWorking = false;
                SimulationButton.Content = "Start simulation";
            }
        }

        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {
            SetVisibilityToCollapsed();
            StatusTextBox.Visibility = Visibility.Visible;
        }

        private void TraderInformationButton_Click(object sender, RoutedEventArgs e)
        {
            SetVisibilityToCollapsed();
            TraderInfoMainGrid.Visibility = Visibility.Visible;
            TraderMessageLabel.Content = string.Empty;
        }

        private void PortfolioInformationButton_Click(object sender, RoutedEventArgs e)
        {
            SetVisibilityToCollapsed();
            PortfoliosMainGrid.Visibility = Visibility.Visible;
            PortfolioMessageLabel.Content = string.Empty;
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

        private void RefreshViewButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
