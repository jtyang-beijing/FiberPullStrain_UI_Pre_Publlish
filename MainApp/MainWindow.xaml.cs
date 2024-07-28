using FiberPullStrain;
using FiberPullStrain.CustomControl.view;
using GLGraphs.CartesianGraph;
using MathNet.Numerics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace FiberPull
{
    public partial class MainWindow : System.Windows.Window {

        public SerialCommunication serialCommunication;
        public MainViewModel viewModel;
        public PublicVars publicVars;
        //List<Point> datapoints = new List<Point>();

        public MainWindow() {
            InitializeComponent(); 
            publicVars = new PublicVars();
            myButtonControls._mainwindow = this;
            myMenuItmes._mainWindow = this;
            CartGraph.Graph = GenerateGraph();
            serialCommunication = new SerialCommunication();
            viewModel = new MainViewModel(serialCommunication);
            this.DataContext = viewModel;
            viewModel.lb_Current_Distance_Content_Changed += ViewModel_lb_Current_Distance_Content_Changed;
            //viewModel.lb_Current_Force_Content_Changed += ViewModel_lb_Current_Force_Content_Changed;

            CartGraph.Graph.State.ItemSelected += State_ItemSelected;
            myButtonControls.generate_Curve_Series += MyButtonControls_generate_Curve_Series;
        }

        private void MyButtonControls_generate_Curve_Series(object sender, EventArgs e)
        {
            CartGraph.Graph.State.IsCameraAutoControlled = true;
            if (publicVars.CURRENT_CURVE_SERIES > 49)
                publicVars.CURRENT_CURVE_SERIES = 0;
            //Create Series, type is Line  ------------------------------
            if (publicVars.LINE_SERIES)
                publicVars.SERIES = CartGraph.Graph.State.AddSeries(SeriesType.Line,
                    $"Series {publicVars.CURRENT_CURVE_SERIES}");
            //Create Series, type is Point.............................
            else
            {
                var r = new Random(50);
                publicVars.SERIES = CartGraph.Graph.State.AddSeries(SeriesType.Point,
                    $"Series {publicVars.CURRENT_CURVE_SERIES}");
                publicVars.SERIES.PointShape = (SeriesPointShape)r.Next((int)SeriesPointShape.InvertedTriangleOutline);
            }
            publicVars.CURRENT_CURVE_SERIES++;
        }


        //private bool delayed_once = false;
        //private async void ViewModel_lb_Current_Force_Content_Changed(object sender, EventArgs e)
        //{
        //    if (datapoints.Count > 0)
        //    {
        //        if (!delayed_once)
        //        {
        //            await Task.Delay(2500);
        //            delayed_once = true;
        //        }
        //        float.TryParse(viewModel.lb_Current_Force, out float y);
        //        datapoints[0] = new Point(datapoints[0].X, y);
        //        AddPoint(datapoints[0]);
        //        datapoints.Remove(datapoints[0]);
        //    }
        //    else
        //    {
        //        delayed_once = false;
        //    }
        //}
        private void ViewModel_lb_Current_Distance_Content_Changed(object sender, EventArgs e)
        {
            if (viewModel.IsRunning)
            {
                float.TryParse(viewModel.lb_Current_Distance, out float x);
                float.TryParse(viewModel.lb_Current_Force, out float y);
                //datapoints.Add(new Point(x,0));
                if(x < float.Parse(publicVars.DESTINATION)) // ignore data when motor returning to zero.
                AddPoint(new Point(x,y));
                float.TryParse(myButtonControls.inBoxDistance.inputBox.Text, out float a);
                if (a == x) viewModel.IsRunning = false;
            }
        }

        public void AddPoint(Point point)
        {
            //var series = CartGraph.Graph.State.AddSeries(SeriesType.Line, 
            //    CartGraph.Graph.State.Series[publicVars.CURRENT_CURVE_SERIES].Name);
            //var series = CartGraph.Graph.State.Series[publicVars.CURRENT_CURVE_SERIES];
            //CartGraph.Graph.State.AddSeries(SeriesType.Line, series.Name);
            var str = point.ToString();
            
            publicVars.SERIES.Add(str, (float)point.X, (float)point.Y);
            //publicVars.SERIES.Add(str, (float)point.X, (float)point.Y);

        }

        public void State_ItemSelected(string obj)
        {
            if (publicVars.LAST_SERIES_ID >= 0)
            {
                CartGraph.Graph.State.Series[publicVars.LAST_SERIES_ID].Color = publicVars.LAST_COLOR;
            }
            string s = CartGraph.Graph.State.MouseoverTarget.Value.Series.Name;
            int i = int.Parse(s.Split(" ")[1]) ;
            publicVars.LAST_SERIES_ID = i;
            publicVars.LAST_COLOR = CartGraph.Graph.State.Series[i].Color;
            CartGraph.Graph.State.Series[i].Color = new Color4(r: 1.0f, g: 0.0f, b: 0.0f, a: 1.0f);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(async () =>
            {
                try
                {
                    await serialCommunication.SearchAllCOMports();
                    InitializeDistance_Force_Box();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during initialization: {ex.Message}");
                }
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void InitializeDistance_Force_Box()
        {
            viewModel.lb_Current_Distance = "-.--";
            viewModel.lb_Current_Force = "-.--";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Exit_FimeWare();
            serialCommunication.ClosePort();
            viewModel.Stop();
            base.OnClosed(e);
        }

        private void Exit_FimeWare()
        {
            if (serialCommunication.myPort.IsOpen) 
            {
                serialCommunication.myPort.Write(publicVars.HOST_CMD_EXIT_FIRMWARE.ToString() + '\n');
            }
        }

        public static CartesianGraph<string> GenerateGraph()
        {
            //const int seriesCount = 50;

            var g = new CartesianGraph<string>();

            var r = new Random(50);

            //for (var i = 0; i < seriesCount; i++)
            //{
            //    var series = g.State.AddSeries(SeriesType.Line, $"Series {i + 1}");
            //    series.PointShape = (SeriesPointShape)r.Next((int)SeriesPointShape.InvertedTriangleOutline);
            //}

            var c = new Color4(r: 1.0f, g: 0.0f, b: 0.0f, a: 0.125f);
            g.State.AddRegion(new Box2(-0.5f, -0.5f, 0.5f, 0.5f), c);
            return g;
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            myMenuItmes.mnClear_Click(sender, e);
        }

        private void AutoZoom_Click(object sender, RoutedEventArgs e)
        {
            CartGraph.Graph.State.IsCameraAutoControlled = true;
        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            myMenuItmes.mnSaveAs_Click(sender, e);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(publicVars.LAST_SERIES_ID >=0)
            {
                CartGraph.Graph.State.Series[publicVars.LAST_SERIES_ID].Clear();
                publicVars.LAST_SERIES_ID = -1;
            }
            else
            {
                MessageBox.Show("No Curve Selected.\nPlease select one Curve.",
                    "Warnning",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
           myMenuItmes.mnOpen_Click(sender, e);
        }
    }
}