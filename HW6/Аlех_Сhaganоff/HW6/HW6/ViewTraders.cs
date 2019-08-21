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
        public void TraderFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource traderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traderViewSource")));
            program.contextProvider.Traders.Load();
            traderViewSource.Source = program.contextProvider.Traders.Local;
            ((CollectionViewSource)this.Resources["traderViewSource"]).View.Refresh();
        }

        private void UpdateTraderButton_Click(object sender, RoutedEventArgs e)
        {
            string traderIdString = TraderIdTradersTextBox.Text;
            string firstNameString = FirstNameTextBox.Text;
            string lastNameString = LastNameTextBox.Text;
            string phoneNumberString = PhoneNumberTextBox.Text;
            string balanceString = BalanceTextBox.Text;

            try
            {
                if (firstNameString.Length > 100)
                {
                    throw new Exception("First name cannot be longer than 100 characters");
                }

                if (lastNameString.Length > 100)
                {
                    throw new Exception("Last name cannot be longer than 100 characters");
                }

                if (phoneNumberString.Length > 50)
                {
                    throw new Exception("Phone number cannot be longer than 50 characters");
                }

                int traderId;
                decimal balance = 0M;

                if (balanceString.Length > 0)
                {
                    balance = Decimal.Parse(balanceString);
                }
                
                if (traderIdString.Length != 0)
                {
                    traderId = Int32.Parse(traderIdString);
                    
                    if (program.dataInteraction.GetTraderCount(traderId) == 0)
                    {
                        throw new Exception("No trader with this Trader ID");
                    }

                    var traderRecordToChange = program.dataInteraction.GetTrader(traderId);

                    if (traderRecordToChange != null)
                    {
                        if(firstNameString.Length > 0)
                        {
                            traderRecordToChange.FirstName = firstNameString;
                        }

                        if (lastNameString.Length > 0)
                        {
                            traderRecordToChange.LastName = lastNameString;
                        }

                        if (phoneNumberString.Length > 0)
                        {
                            traderRecordToChange.PhoneNumber = phoneNumberString;
                        }

                        if (balanceString.Length > 0)
                        {
                            traderRecordToChange.Balance = balance;
                        }
                    }

                    program.dataInteraction.SaveChanges();

                    program.outputProvider.WriteLine("Trader record updated");
                    program.logger.Write("Trader record updated");
                }
                else
                {
                    if (firstNameString.Length == 0)
                    {
                        throw new Exception("First name cannot be empty");
                    }

                    if (lastNameString.Length == 0)
                    {
                        throw new Exception("Last name cannot be empty");
                    }

                    program.dataInteraction.AddTrader(firstNameString, lastNameString, phoneNumberString, balance);

                    program.dataInteraction.SaveChanges();

                    program.outputProvider.WriteLine("Trader record created");
                    program.logger.Write("Trader record created");
                }
            }
            catch(Exception ex)
            {
                program.outputProvider.WriteLine(ex.Message);
                program.logger.Write(ex.Message);
                TraderMessageLabel.Content = ex.Message;
            }
            finally
            {
                TraderIdTradersTextBox.Text = String.Empty;
                FirstNameTextBox.Text = String.Empty;
                LastNameTextBox.Text = String.Empty;
                PhoneNumberTextBox.Text = String.Empty;
                BalanceTextBox.Text = String.Empty;

                UpdateView();
            }
        }

        private void TraderDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int traderId = (traderDataGrid.SelectedItem as Trader).TraderId;

            var trader = program.dataInteraction.GetTrader(traderId);

            program.dataInteraction.RemoveTrader(trader);
            program.dataInteraction.SaveChanges();
            program.outputProvider.WriteLine("Trader deleted");
            program.logger.Write("Trader deleted");
        }

        private void TradersLowerGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            TraderMessageLabel.Content = string.Empty;
        }

        private void TraderIdTradersTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TraderMessageLabel.Content = string.Empty;
        }

        private void PhoneNumberTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TraderMessageLabel.Content = string.Empty;
        }

        private void FirstNameTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TraderMessageLabel.Content = string.Empty;
        }

        private void LastNameTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TraderMessageLabel.Content = string.Empty;
        }

        private void BalanceTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TraderMessageLabel.Content = string.Empty;
        }
    }
}
