using FiberPullStrain;
using FiberPullStrain.CustomControl.view;
using GLGraphs.CartesianGraph;
using MathNet.Numerics;
using OpenTK.Mathematics;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace FiberPull
{
    public partial class MainWindow : System.Windows.Window {

        public SerialCommunication serialCommunication;
        public MainViewModel viewModel;
        public PublicVars publicVars;

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

            CartGraph.Graph.State.ItemSelected += State_ItemSelected;
        }

        Point newPoint = new Point();
        private void ViewModel_lb_Current_Distance_Content_Changed(object sender, EventArgs e)
        {
            if (viewModel.IsRunning)
            {
                float.TryParse(viewModel.lb_Current_Distance, out float x);
                float.TryParse(viewModel.lb_Current_Force, out float y);
                newPoint.X = x; newPoint.Y = y;
                AddPoint(newPoint);
                float.TryParse(myButtonControls.inBoxDistance.inputBox.Text, out float a);
                if (a == x) viewModel.IsRunning = false;
            }
        }

        public void AddPoint(Point point)
        {
            var series = CartGraph.Graph.State.Series[publicVars.CURRENT_CURVE_SERIES];
            var str = point.ToString();
            series.Add(str, (float)point.X, (float)point.Y);
        }

        private void State_ItemSelected(string obj)
        {
            if (publicVars.LAST_SERIES_ID >= 0)
            {
                CartGraph.Graph.State.Series[publicVars.LAST_SERIES_ID].Color = publicVars.LAST_COLOR;
            }
            string s = CartGraph.Graph.State.MouseoverTarget.Value.Series.Name;
            int i = int.Parse(s.Split(" ")[1]) - 1;
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
                serialCommunication.myPort.WriteLine(publicVars.HOST_CMD_EXIT_FIRMWARE.ToString());
            }
        }

        public static CartesianGraph<string> GenerateGraph()
        {
            const int seriesCount = 50;

            var g = new CartesianGraph<string>();

            var r = new Random(50);

            for (var i = 0; i < seriesCount; i++)
            {
                var series = g.State.AddSeries(SeriesType.Point, $"Series {i + 1}");
                series.PointShape = (SeriesPointShape)r.Next((int)SeriesPointShape.InvertedTriangleOutline);
            }

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
            CartGraph.Graph.State.Series[publicVars.LAST_SERIES_ID].Clear();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
           myMenuItmes.mnOpen_Click(sender, e);
        }
    }
}