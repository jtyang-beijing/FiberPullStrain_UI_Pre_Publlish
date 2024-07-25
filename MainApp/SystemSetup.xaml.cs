using FiberPull;
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
using System.Windows.Shapes;

namespace FiberPullStrain
{
    /// <summary>
    /// Interaction logic for SystemSetup.xaml
    /// </summary>
    public partial class SystemSetup : Window
    {
        private MainWindow _mainWindow { get; set; }
        public SystemSetup()
        {
            InitializeComponent();
            _mainWindow = Application.Current.MainWindow as MainWindow;
        }

        private void calibrate_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = 
            MessageBox.Show("Following these steps to Run Force Sensor Calibration:\n\n" +
                "1. Attach a Known load onto the stage sensor\n" +
                "2. Input real load in Grams into text box\n" +
                "3. Press Finish Button.\n\n" +
                "Please check informations from Bottom of Main Winodw\n" +
                "during operation.", "Information", MessageBoxButton.OKCancel,
                MessageBoxImage.Information);
            if (res == MessageBoxResult.OK) 
            {
                calibrate_load_cell();
                real_weight.IsEnabled = true; 
                finish.IsEnabled = true;
            }
            else { }
        }
        private void calibrate_load_cell()
        {
            if(_mainWindow.serialCommunication.myPort.IsOpen)
            {
                try 
                {
                    _mainWindow.serialCommunication.myPort.WriteLine
                        (_mainWindow.publicVars.HOST_CMD_CALIBRATE_LOAD_SENSOR.ToString());
                    MessageBox.Show("Now attach load onto stage sensor and then continue...\n\n" + 
                        "DO NOT REMOVE LOAD Until input real load and press Finish Button.",
                        "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch(Exception err)
                {
                    MessageBox.Show($"Error: {err.Message}");
                }
            }
        }

        private void save_calibrate()
        {
            if (_mainWindow.serialCommunication.myPort.IsOpen)
            {
                try
                {
                    _mainWindow.serialCommunication.myPort.WriteLine(real_weight.Text);
                    Thread.Sleep(1000);
                    _mainWindow.serialCommunication.myPort.Write("y");
                }
                catch (Exception err)
                {
                    MessageBox.Show($"Error: {err.Message}");
                }
            }
        }

        private void cancle_calibrate()
        {
            if (_mainWindow.serialCommunication.myPort.IsOpen)
            {
                try
                {
                    _mainWindow.serialCommunication.myPort.WriteLine("n");
                }
                catch (Exception err)
                {
                    MessageBox.Show($"Error: {err.Message}");
                }
            }
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res =
            MessageBox.Show("Are you Sure to save the calibration?", 
            "Information", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (res == MessageBoxResult.OK)
            {
                save_calibrate();
                MessageBox.Show("Calibration Succeed.", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else 
            { 
                cancle_calibrate();
                MessageBox.Show("Calibration was not stored.","Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            this.Close();
        }
    }
}
