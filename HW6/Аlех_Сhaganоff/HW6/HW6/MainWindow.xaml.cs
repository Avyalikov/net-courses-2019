using HW6.Classes;
using HW6.Interfaces;
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
        public Program program;
        
        public MainWindow()
        {
            ISimulation simulation = new Simulation();
            IOutputProvider outputProvider = new OutputToStatusTextBox();

            program = new Program(simulation, outputProvider);
            
            InitializeComponent();

            AppWindow = this;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (program.SimulationIsWorking==false)
            {
                program.outputProvider.WriteLine("Simulation started");
                program.SimulationIsWorking = true;
                SimulationButton.Content = "Stop simulation";

                program.simulation.TradingSimulation(App.context, program, program.outputProvider);  
            }
            else
            {
                program.outputProvider.WriteLine("Simulation ended");
                program.SimulationIsWorking = false;
                SimulationButton.Content = "Start simulation";
            }
        }

        private void TraderInformationButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBox.Visibility = Visibility.Collapsed;
            TraderInfoMainGrid.Visibility = Visibility.Visible;
        }

        private void SimulationViewButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBox.Visibility = Visibility.Visible;
            TraderInfoMainGrid.Visibility = Visibility.Collapsed;
        }
    }
}
