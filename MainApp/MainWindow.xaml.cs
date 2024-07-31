using FiberPullStrain;
using FiberPullStrain.CustomControl.view;
using GLGraphs.CartesianGraph;
using OpenTK.Mathematics;
using System;
using System.Windows;

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
            viewModel.lb_Current_Force_Content_Changed += ViewModel_lb_Current_Force_Content_Changed;

            CartGraph.Graph.State.ItemSelected += State_ItemSelected;
            myButtonControls.generate_Curve_Series += MyButtonControls_generate_Curve_Series;
        }

        //public void ViewModel_lb_Current_Force_Content_Changed(object sender, EventArgs e)
        //{
        //    publicVars.CURRENT_FORCE = viewModel.lb_Current_Force;
        //    if (viewModel.IsRunning)
        //    {
        //        float.TryParse(viewModel.lb_Current_Force, out float current_force);
        //        float.TryParse(myButtonControls.inBoxForce.inputBox.Text, out float target_force);
        //        float.TryParse(publicVars.CURRENT_DISTANCE, out float current_distance);
        //        //float.TryParse(publicVars.DESTINATION, out float destination);
        //        if (current_force >= target_force && publicVars.MOVE_FORWARD && !string.IsNullOrEmpty(myButtonControls.inBoxForce.inputBox.Text))
        //        {
        //            serialCommunication.myPort.Write(publicVars.HOST_CMD_STOP_MOTOR.ToString() + '\n');
        //            viewModel.IsRunning = false;
        //        }
        //        AddPoint(new Point(current_distance, current_force));
        //        if(current_force >= target_force) viewModel.IsRunning = false;
        //    }
        //}

        public void ViewModel_lb_Current_Force_Content_Changed(object sender, EventArgs e)
        {
            publicVars.CURRENT_FORCE = viewModel.lb_Current_Force;
            if (viewModel.IsRunning)
            {
                float.TryParse(publicVars.CURRENT_FORCE, out float current_force);
                float.TryParse(publicVars.TARGET_FORCE, out float target_force);
                //float.TryParse(publicVars.CURRENT_DISTANCE, out float current_distance);
                //float.TryParse(publicVars.DESTINATION, out float destination);
                if (current_force >= target_force && publicVars.MOVE_FORWARD && !string.IsNullOrEmpty(myButtonControls.inBoxForce.inputBox.Text))
                {
                    stop_motor();
                    viewModel.IsRunning = false;
                }
                //AddPoint(new Point(current_distance, current_force));
                //if (current_force >= target_force) viewModel.IsRunning = false;
            }
        }
        public void MyButtonControls_generate_Curve_Series(object sender, EventArgs e)
        {
            CartGraph.Graph.State.IsCameraAutoControlled = true;
            string name =$"Series {publicVars.CURRENT_CURVE_SERIES}";
            Generate_Curve_Series(name);
        }

        public void Generate_Curve_Series(string name)
        {
            publicVars.CURRENT_CURVE_SERIES++; // initial value is -1
            //Create Series, type is Line  ------------------------------
            if (publicVars.LINE_SERIES)
                publicVars.SERIES = CartGraph.Graph.State.AddSeries(SeriesType.Line,name);
            //Create Series, type is Point.............................
            else
            {
                var r = new Random(50);
                publicVars.SERIES = CartGraph.Graph.State.AddSeries(SeriesType.Point,name);
                publicVars.SERIES.PointShape = (SeriesPointShape)r.Next((int)SeriesPointShape.InvertedTriangleOutline);
            }
            publicVars.CURVE_SERIES.Add(name, publicVars.CURRENT_CURVE_SERIES); //store curve info into Dictionary.
        }

        public void ViewModel_lb_Current_Distance_Content_Changed(object sender, EventArgs e)
        {
            publicVars.CURRENT_DISTANCE = viewModel.lb_Current_Distance;
            if (viewModel.IsRunning)
            {
                float.TryParse(publicVars.CURRENT_DISTANCE, out float current_distance);
                float.TryParse(publicVars.CURRENT_FORCE, out float current_force);
                float.TryParse(publicVars.DESTINATION, out float destination);
                if (publicVars.MOVE_FORWARD) // ignore data when motor returning to zero.
                    AddPoint(new Point(current_distance, current_force));
                if (current_distance == destination) viewModel.IsRunning = false;
            }
        }

        public void AddPoint(Point point)
        {
            var str = point.ToString();
            publicVars.SERIES.Add(str, (float)point.X, (float)point.Y);
        }

        public void State_ItemSelected(string obj)
        {
            if (publicVars.LAST_SERIES_ID >= 0)
            {
                CartGraph.Graph.State.Series[publicVars.LAST_SERIES_ID].Color = publicVars.LAST_COLOR;
            }
            string s = CartGraph.Graph.State.MouseoverTarget.Value.Series.Name;
            publicVars.LAST_SERIES_ID = publicVars.CURVE_SERIES[s];//get series ID from Dictionary.
            publicVars.LAST_COLOR = CartGraph.Graph.State.Series[publicVars.LAST_SERIES_ID].Color;
            CartGraph.Graph.State.Series[publicVars.LAST_SERIES_ID].Color = new Color4(r: 1.0f, g: 0.0f, b: 0.0f, a: 1.0f);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(async () =>
            {
                InitializeDistance_Force_Box();

                try
                {
                    string portName = publicVars.read_from_registry("COM Port", "Port", "COM1");
                    await serialCommunication.HandShakeWithPort(portName);
                    if(!publicVars.HANDSHAKESUCCEED)
                    await serialCommunication.SearchAllCOMports();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during initialization: {ex.Message}");
                }
                if (publicVars.HANDSHAKESUCCEED) 
                {
                    InitKeyVars();
                
                }
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void InitKeyVars()
        {
            string speed = publicVars.read_from_registry("Motor", "Speed", "100");
            string acc = publicVars.read_from_registry("Motor", "Acceloration", "100");
            string max_distance = publicVars.read_from_registry("Motor", "Max Distance", "26");
            string max_load = publicVars.read_from_registry("Sensor", "Max Load", "1000");
            string scale = publicVars.read_from_registry("Motor", "Scale", "909.09090909");
            publicVars.set_MaxValues(max_distance, max_load);
            publicVars.set_motor_acceloration(acc);
            publicVars.set_motor_scale(scale);
            publicVars.set_motor_speed(speed);
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

        public void run_motor(string destination)
        {
            try
            {
                string _cmd = publicVars.HOST_CMD_DRIVE_MOTOR +
                        (decimal.Parse(destination) * publicVars.MOTOR_SCALE).ToString() + '\n';
                serialCommunication.myPort.Write(_cmd);

            }
            catch(Exception exp)
            {
                MessageBox.Show($"Error try running motor: {exp.Message}");
            }
        }

        public void stop_motor()
        {
            try
            {
                serialCommunication.myPort.Write(publicVars.HOST_CMD_STOP_MOTOR.ToString() + '\n');
            }
            catch(Exception exp)
            {
                MessageBox.Show($"Error try stopping motor: {exp.Message}");
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
            myMenuItmes.mnClearView_Click(sender, e);
        }

        private void AutoZoom_Click(object sender, RoutedEventArgs e)
        {
            CartGraph.Graph.State.IsCameraAutoControlled = true;
        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            myMenuItmes.mnSaveAs_Click(sender, e);
        }

        private void Delete_Selected_Curve_Click(object sender, RoutedEventArgs e)
        {
            myMenuItmes.mnDeleteSelectedCurve_Click(sender,e);
            
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
           myMenuItmes.mnOpen_Click(sender, e);
        }

        private void systemSetup_Click(object sender, RoutedEventArgs e)
        {
            myMenuItmes.mnSystemSetup_Click(sender, e);
        }

        private void SetCurveStyle_Click(object sender, RoutedEventArgs e)
        {
            myMenuItmes.mnView.IsSubmenuOpen = true;
            myMenuItmes.mnSetCurveStyle.IsSubmenuOpen = true;
            myMenuItmes.checkboxlineCuvre.Focus();
        }

        private void jogleft_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            publicVars.MOVE_FORWARD = false;
            run_motor("0.00");
        }

        private void jogleft_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            stop_motor();
        }
        public bool jog_button_runonce = false;
        private void jogright_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!jog_button_runonce) 
            {
                MyButtonControls_generate_Curve_Series(sender,e);
                jog_button_runonce=true;
            }
            viewModel.IsRunning = true;
            publicVars.MOVE_FORWARD = true;
            run_motor(publicVars.MAX_VALUE_DISTANCE);
        }

        private void jogright_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            viewModel.IsRunning = false;
            stop_motor();
        }
    }
}