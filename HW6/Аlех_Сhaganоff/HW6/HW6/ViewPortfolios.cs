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

namespace HW6
{
    public partial class MainWindow : Window
    {
        private void UpdatePortfolioButton_Click(object sender, RoutedEventArgs e)
        {
            string traderIdString = TraderIdTextBox.Text;
            string shareIdString = ShareIdTextBox.Text;
            string quantityString = QuantityTextBox.Text;

            try
            {
                if (traderIdString.Length == 0)
                {
                    throw new Exception("Trader ID cannot be empty");
                }

                if (shareIdString.Length == 0)
                {
                    throw new Exception("Share ID cannot be empty");
                }

                if (quantityString.Length == 0)
                {
                    throw new Exception("Quantity cannot be empty");
                }

                int traderId = Int32.Parse(traderIdString);
                int shareId = Int32.Parse(shareIdString);
                int quantity = Int32.Parse(quantityString);

                if(program.dataInteraction.GetTraderCount(traderId) == 0)
                {
                    throw new Exception("No trader with this Trader ID");
                }

                if (program.dataInteraction.GetShareCount(shareId) == 0)
                {
                    throw new Exception("No share type with this Share ID");
                }

                if (quantity == 0 || quantity < 0)
                {
                    throw new Exception("Quantity cannot be zero or negative number");
                }

                if (program.dataInteraction.GetPortfoliosCount(traderId, shareId) > 0)
                {
                    var buyerShareRecordToChange = program.dataInteraction.GetPortfolio(traderId, shareId);

                    if (buyerShareRecordToChange != null)
                    {
                        buyerShareRecordToChange.Quantity = quantity;
                    }

                    program.dataInteraction.SaveChanges();

                    program.outputProvider.WriteLine("Porftolio record updated");
                    program.logger.Write("Porftolio record updated");
                }
                else
                {
                    program.dataInteraction.AddPortfolio(traderId, shareId, quantity);

                    program.outputProvider.WriteLine("Added new record to portfolio");
                    program.logger.Write("Added new record to portfolio");
                    PortfolioMessageLabel.Content = string.Empty;
                    program.dataInteraction.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                program.outputProvider.WriteLine(ex.Message);
                program.logger.Write(ex.Message);
                PortfolioMessageLabel.Content = ex.Message;
            }
            finally
            {
                TraderIdTextBox.Text = String.Empty;
                ShareIdTextBox.Text = String.Empty;
                QuantityTextBox.Text = "1";

                UpdateView();
            }
        }

        private void PortfolioDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int traderId = (portfolioDataGrid.SelectedItem as Portfolio).TraderID;
            int shareId = (portfolioDataGrid.SelectedItem as Portfolio).ShareId;

            var portfolio = program.dataInteraction.GetPortfolio(traderId, shareId);

            program.dataInteraction.RemovePortfolio(portfolio);
            program.dataInteraction.SaveChanges();
            program.outputProvider.WriteLine("Portfolio deleted");
            program.logger.Write("Portfolio deleted");
        }

        private void PortfoliosLowerGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            PortfolioMessageLabel.Content = string.Empty;
        }

        private void TraderIdTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            PortfolioMessageLabel.Content = string.Empty;
        }

        private void ShareIdTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            PortfolioMessageLabel.Content = string.Empty;
        }

        private void QuantityTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            PortfolioMessageLabel.Content = string.Empty;
        }
    }
}