using FiberPull;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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

        public void mnClear_Click(object sender, RoutedEventArgs e)
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

        public void mnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.Filter = "Curve File | *.cuv";
            ofd.Title = "Open Curve File";
            bool? res = ofd.ShowDialog();
            if (res == true)
            {
                try 
                { 
                    var dataPoints = File.ReadAllLines(ofd.FileName);
                    _mainWindow.publicVars.CURRENT_CURVE_SERIES++;
                    _mainWindow.CartGraph.Graph.State.IsCameraAutoControlled = true;
                    if (_mainWindow.publicVars.CURRENT_CURVE_SERIES > 49) _mainWindow.publicVars.CURRENT_CURVE_SERIES = 0;
                    Point newPoint = new();
                    foreach (var dataPoint in dataPoints)
                    {
                        string[] p = dataPoint.Split(',');
                        newPoint.X = float.Parse(p[0].ToString());
                        newPoint.Y = float.Parse(p[1].ToString());
                        _mainWindow.AddPoint(newPoint);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void mnSave_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                int seriesID = _mainWindow.publicVars.LAST_SERIES_ID;
                if (seriesID < 0) seriesID = _mainWindow.publicVars.CURRENT_CURVE_SERIES;
                string fn = "curve.crv";
                var datapoints = _mainWindow.CartGraph.Graph.State.Series[seriesID].Points.ToList();
                List<string> myList = new List<string>();
                foreach (var datapoint in datapoints) { myList.Add(datapoint.Value.ToString()); }
                File.WriteAllLines(fn, myList);
                MessageBox.Show("Current Curve was saved as \n" +
                    fn + "\n" + "to " + AppContext.BaseDirectory.ToString(),
                    "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception err)
            { MessageBox.Show(err.Message,"Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        public void mnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefile = new();
            savefile.Filter = "Curve File | *.cuv";
            savefile.Title = "Save file as";
            bool? res = savefile.ShowDialog();
            if (res == true) 
            {
                int seriesID = _mainWindow.publicVars.LAST_SERIES_ID;
                if (seriesID < 0) seriesID = _mainWindow.publicVars.CURRENT_CURVE_SERIES;
                string fn = savefile.FileName;
                var datapoints = _mainWindow.CartGraph.Graph.State.Series[seriesID].Points.ToList();
                List<string> myList = new List<string>();
                foreach (var datapoint in datapoints) { myList.Add(datapoint.Value.ToString()); }
                File.WriteAllLines(fn, myList);
            }
        }
    }
}
