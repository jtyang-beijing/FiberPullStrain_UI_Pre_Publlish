using FiberPullStrain;
using FiberPullStrain.CustomControl.view;
using GLGraphs.CartesianGraph;
using OpenTK.Mathematics;
using System;
using System.Windows;

namespace FiberPull
{
    public partial class MainWindow : Window {

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
            
        }

        Point newPoint = new Point();
        private void ViewModel_lb_Current_Distance_Content_Changed(object sender, EventArgs e)
        {
            float.TryParse(viewModel.lb_Current_Distance, out float x);
            float.TryParse(viewModel.lb_Current_Force, out float y);
            newPoint.X = x; newPoint.Y = y;
            AddPoint(newPoint);
            float.TryParse(myButtonControls.inBoxDistance.inputBox.Text, out float a);
            if (a == x) viewModel.IsRunning = false;
        }

        //private int i = 0;
        public void AddPoint(Point point)
        {
            var series = CartGraph.Graph.State.Series[publicVars.CURRENT_CURVE_SERIES];
            var str = point.ToString();
            series.Add(str, (float)point.X, (float)point.Y);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await serialCommunication.SearchAllCOMports();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (serialCommunication.myPort.IsOpen) 
                serialCommunication.myPort.Close();
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
    }
}