using HW6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW6.Classes
{
    public class OutputToStatusTextBox : IOutputProvider
    {
        public virtual void WriteLine(String text)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow.AppWindow.StatusTextBox.AppendText(text + Environment.NewLine);
            });
        }
    }
}
