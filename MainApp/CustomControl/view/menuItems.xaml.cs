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
            await Task.Delay(100);
            while(!_mainWindow.publicVars.HANDSHAKESUCCEED)
            await _mainWindow.serialCommunication.SearchAllCOMports();
        }

        public void mnSystemSetup_Click(object sender, RoutedEventArgs e)
        {
            SystemSetup systemSetup = new SystemSetup();
            systemSetup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            systemSetup.ShowDialog();
        }

        private void openCurveFile(string filename)
        {
            try
            {
                _mainWindow.Generate_Curve_Series(filename);
                var dataPoints = File.ReadAllLines(filename);
                _mainWindow.CartGraph.Graph.State.IsCameraAutoControlled = true;
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

        private string saveCurveFile(string filename)
        {
            int seriesID = _mainWindow.publicVars.LAST_SERIES_ID;
            if (seriesID < 0) seriesID = _mainWindow.publicVars.CURRENT_CURVE_SERIES;
            if (string.IsNullOrEmpty(filename)) filename = _mainWindow.CartGraph.Graph.State.Series[seriesID].Name;
            var datapoints = _mainWindow.CartGraph.Graph.State.Series[seriesID].Points.ToList();
            List<string> myList = new List<string>();
            foreach (var datapoint in datapoints) { myList.Add(datapoint.Value.ToString()); }
            File.WriteAllLines(filename, myList);
            return filename;
        }
        public void mnDeleteSelectedCurve_Click(object sender, RoutedEventArgs e)
        {
            if (_mainWindow.publicVars.LAST_SERIES_ID >= 0)
            {
                string curveName = _mainWindow.CartGraph.Graph.State.Series[_mainWindow.publicVars.LAST_SERIES_ID].Name;
                _mainWindow.CartGraph.Graph.State.Series[_mainWindow.publicVars.LAST_SERIES_ID].Clear();
                _mainWindow.publicVars.LAST_SERIES_ID = -1;
                _mainWindow.CartGraph.Graph.State.IsCameraAutoControlled = true;
                _mainWindow.publicVars.CURVE_SERIES.Remove(curveName);
            }
            else
            {
                MessageBox.Show("No Curve Selected.\nPlease select one Curve.",
                    "Warnning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void mnClearView_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in _mainWindow.publicVars.CURVE_SERIES.Values)
            {
                _mainWindow.CartGraph.Graph.State.Series[item].Clear();
            }
            _mainWindow.CartGraph.Graph.State.IsCameraAutoControlled = true;
            _mainWindow.publicVars.CURVE_SERIES.Clear();// clear Dictionary.
        }

        public void mnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.Filter = "Curve File | *.cuv|All Files | *.*";
            ofd.Multiselect = true;
            ofd.Title = "Open Curve File";
            bool? res = ofd.ShowDialog();
            if (res == true)
            {
                foreach (string filename in ofd.FileNames) 
                {
                    openCurveFile(filename);
                }
            }
        }

        private void mnSave_Click(object sender, RoutedEventArgs e)
        {
            //try 
            //{
            //    string filename = saveCurveFile(null);
            //    MessageBox.Show($"Current Curve {filename} was saved to\n" +
            //        AppContext.BaseDirectory.ToString(),
            //        "Information",
            //        MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //catch (Exception err)
            //{ MessageBox.Show(err.Message,"Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            mnSaveAs_Click(sender,e);
        }

        public void mnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefile = new();
            savefile.Filter = "Curve File | *.cuv";
            savefile.Title = "Save file as";
            bool? res = savefile.ShowDialog();
            if (res == true) 
            {
                saveCurveFile(savefile.FileName);
                //update CURVE_SERIERS dictionary and display
                int seriesID = _mainWindow.publicVars.LAST_SERIES_ID;
                if (seriesID < 0) seriesID = _mainWindow.publicVars.CURRENT_CURVE_SERIES;
                string keyName = _mainWindow.CartGraph.Graph.State.Series[seriesID].Name;
                _mainWindow.publicVars.CURVE_SERIES.Remove(keyName);
                _mainWindow.CartGraph.Graph.State.Series[seriesID].Clear();
                openCurveFile(savefile.FileName); // dictionary will be update when openning file.
            }
        }

        public void checkboxlineCuvre_Click(object sender, RoutedEventArgs e)
        {
            checkboxdotsCuvre.IsChecked = !checkboxdotsCuvre.IsChecked;
            if (checkboxlineCuvre.IsChecked == true)
            {
                _mainWindow.publicVars.LINE_SERIES = true;
            }
            else
            {
                _mainWindow.publicVars.LINE_SERIES = false;

            }
        }

        public void checkboxdotsCuvre_Click(object sender, RoutedEventArgs e)
        {
            checkboxlineCuvre.IsChecked = !checkboxlineCuvre.IsChecked;
            if (checkboxdotsCuvre.IsChecked == true)
            {
                _mainWindow.publicVars.LINE_SERIES = false;
            }
            else 
            { 
                _mainWindow.publicVars.LINE_SERIES = true;
            }
        }

        public void mnNewJog_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.jog_button_runonce = false;
            _mainWindow.jogright.Focus();
        }
    }
}
