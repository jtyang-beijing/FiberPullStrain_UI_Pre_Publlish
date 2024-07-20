using FiberPull;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FiberPullStrain.CustomControl.view
{
    public partial class menuItems : UserControl
    {
        public MainWindow _mainWindow { get; set; }
        public menuItems()
        {
            InitializeComponent(); 
            
        }

        private void mnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void mnAboutProduct_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("In-Suit Fiber Pull Strain Test Stage" +
                "\nVersion 1.0.0", "Information",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void mnAboutSupport_Click(object sender, RoutedEventArgs e)
        {
            About about = new About
            {
                Width = 500,
                Height = 500
            };
            about.Show();
        }

        private async void mnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (_mainWindow.serialCommunication.myPort.IsOpen)
            {
                try
                {
                    _mainWindow.serialCommunication.myPort.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            await Task.Delay(300);
            await _mainWindow.serialCommunication.SearchAllCOMports();
        }

        private void mnClear_Click(object sender, RoutedEventArgs e)
        {
            for(int i= 0; i <= _mainWindow.publicVars.CURRENT_CURVE_SERIES; i++)
            {
                _mainWindow.CartGraph.Graph.State.Series[i].Clear();
            }
            //_mainWindow.CartGraph.Graph.State.Camera.Current.ZoomIn(0);
            _mainWindow.CartGraph.Graph.State.IsCameraAutoControlled = true;
        }

        private void mnClearCurrent_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.CartGraph.Graph.State.Series[_mainWindow.publicVars.CURRENT_CURVE_SERIES].Clear();
            _mainWindow.publicVars.CURRENT_CURVE_SERIES--;
        }
    }
}
