using FiberPull;
using GLGraphs.CartesianGraph;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace FiberPullStrain.CustomControl.view
{
    public partial class ButtonControls : UserControl
    {
        public MainWindow _mainwindow { get; set; }
        //private int ViewModel_lb_Current_Distance_Content_Changed { get; }
        public ButtonControls()
        {
            InitializeComponent(); 
            // initialized max value of input box. 
            this.Loaded += ButtonControls_Loaded;
            // use this booking event to avoid _mainwindow.publicVars initializing problem.
         
        } 

        private void ButtonControls_Loaded(object sender, RoutedEventArgs e)
        {
            _mainwindow = Application.Current.MainWindow as MainWindow;
            if (_mainwindow != null)
            {
                inBoxDistance.MaxValue = _mainwindow.publicVars.MAX_VALUE_DISTANCE;
                inBoxDistance.MinValue = "-" + _mainwindow.publicVars.MAX_VALUE_DISTANCE;
                inBoxForce.MaxValue = _mainwindow.publicVars.MAX_VALUE_FORCE;

                inBoxDistance.tbPlaceHolder.Text = "input distance";
                inBoxForce.tbPlaceHolder.Text = "input Force";
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if(_mainwindow.serialCommunication.myPort.IsOpen)
            {
                //_mainwindow.serialCommunication.myPort.DiscardInBuffer();
                //_mainwindow.serialCommunication.myPort.DiscardOutBuffer();
                _mainwindow.serialCommunication.myPort.Close();
            }
            App.Current.Shutdown();
        }
        public event EventHandler generate_Curve_Series;
        private void btStart_Click(object sender, RoutedEventArgs e)
        {

            //_mainwindow.viewModel.lb_Current_Distance_Content_Changed -= _mainwindow.ViewModel_lb_Current_Distance_Content_Changed;
            //_mainwindow.viewModel.lb_Current_Force_Content_Changed -= _mainwindow.ViewModel_lb_Current_Force_Content_Changed;
            if (_mainwindow.viewModel.IsRunning) 
            {
                _mainwindow.serialCommunication.myPort.Write(
                    _mainwindow.publicVars.HOST_CMD_STOP_MOTOR.ToString() + '\n');
                _mainwindow.viewModel.IsRunning = false;
            }
            else 
            {
                _mainwindow.myMenuItmes.mnNew_Click(sender,e); // re-create Curve Series
                // store destination to public varialble.
                _mainwindow.publicVars.DESTINATION = inBoxDistance.inputBox.Text;
                _mainwindow.publicVars.TARGET_FORCE = inBoxForce.inputBox.Text;
                float.TryParse(_mainwindow.publicVars.CURRENT_DISTANCE, out float current_distance);
                float.TryParse(_mainwindow.publicVars.DESTINATION, out float destination);
                //float.TryParse(inBoxForce.inputBox.Text, out float target_force);
                //float.TryParse(_mainwindow.publicVars.CURRENT_FORCE, out float current_force);

                if (destination > current_distance)
                {
                    generate_Curve_Series?.Invoke(this, EventArgs.Empty);
                    _mainwindow.publicVars.MOVE_FORWARD = true;
                    //_mainwindow.viewModel.lb_Current_Distance_Content_Changed += _mainwindow.ViewModel_lb_Current_Distance_Content_Changed;
                }
                else _mainwindow.publicVars.MOVE_FORWARD = false;

                //if (target_force > current_force && string.IsNullOrEmpty(inBoxDistance.inputBox.Text))
                //{
                //    destination = float.Parse(_mainwindow.publicVars.MAX_VALUE_DISTANCE);
                //    generate_Curve_Series?.Invoke(this, EventArgs.Empty);
                //    _mainwindow.publicVars.MOVE_FORWARD = true;
                //    _mainwindow.viewModel.lb_Current_Force_Content_Changed += _mainwindow.ViewModel_lb_Current_Force_Content_Changed;
                //}
                //else if(target_force < current_force && !string.IsNullOrEmpty(inBoxForce.inputBox.Text))
                //{
                //    destination = 0;
                //    _mainwindow.publicVars.MOVE_FORWARD = false;
                //}

                // send command to Arduino, drive motor to destinaton.
                _mainwindow.run_motor(_mainwindow.publicVars.DESTINATION);
                _mainwindow.viewModel.IsRunning = !_mainwindow.viewModel.IsRunning;
                if (destination == current_distance) _mainwindow.viewModel.IsRunning = false;
            }
        }

        private void cbmm_Click(object sender, RoutedEventArgs e)
        {
            cbinch.IsChecked = !cbinch.IsChecked;
            bool ok = Decimal.TryParse(inBoxDistance.inputBox.Text, out Decimal ss);
            if (ok) 
            {
                if (cbmm.IsChecked == true)
                {
                    inBoxDistance.MaxValue = _mainwindow.publicVars.MAX_VALUE_DISTANCE;
                    inBoxDistance.inputBox.Text = (ss * _mainwindow.publicVars.DISTANCE_EXCHANGE_RATE).ToString("F2");
                }
                else
                {
                    inBoxDistance.MaxValue = (Decimal.Parse(_mainwindow.publicVars.MAX_VALUE_DISTANCE) /
                        _mainwindow.publicVars.DISTANCE_EXCHANGE_RATE ).ToString("F2");
                    inBoxDistance.inputBox.Text = (ss / _mainwindow.publicVars.DISTANCE_EXCHANGE_RATE).ToString("F2");
                }
            }
        }

        private void cbinch_Click(object sender, RoutedEventArgs e)
        {
            cbmm.IsChecked = !cbmm.IsChecked;
            bool ok = Decimal.TryParse(inBoxDistance.inputBox.Text, out Decimal ss);
            if(ok)
            {
                if (cbinch.IsChecked == true)
                {
                    inBoxDistance.MaxValue = (Decimal.Parse(_mainwindow.publicVars.MAX_VALUE_DISTANCE) /
                        _mainwindow.publicVars.DISTANCE_EXCHANGE_RATE).ToString("F2");
                    inBoxDistance.inputBox.Text = (ss / _mainwindow.publicVars.DISTANCE_EXCHANGE_RATE).ToString("F2");

                }
                else
                {
                    inBoxDistance.MaxValue = _mainwindow.publicVars.MAX_VALUE_DISTANCE;
                    inBoxDistance.inputBox.Text = (ss * _mainwindow.publicVars.DISTANCE_EXCHANGE_RATE).ToString("F2");
                }
            }

        }

        private void cbgrams_Click(object sender, RoutedEventArgs e)
        {
            cbnewton.IsChecked = ! cbnewton.IsChecked;
            bool ok = Decimal.TryParse(inBoxForce.inputBox.Text, out Decimal ss);
            if(ok)
            {
                if (cbgrams.IsChecked == true)
                {
                    inBoxForce.MaxValue = _mainwindow.publicVars.MAX_VALUE_FORCE;
                    inBoxForce.inputBox.Text = (ss * _mainwindow.publicVars.FORCE_EXCHANGE_RATE).ToString("F2");
                }
                else
                {
                    inBoxForce.MaxValue = (Decimal.Parse(_mainwindow.publicVars.MAX_VALUE_FORCE) /
                        _mainwindow.publicVars.FORCE_EXCHANGE_RATE).ToString("F2");
                    inBoxForce.inputBox.Text = (ss / _mainwindow.publicVars.FORCE_EXCHANGE_RATE).ToString("F2");
                }
            }
        }

        private void cbnewton_Click(object sender, RoutedEventArgs e)
        {
            cbgrams.IsChecked = !cbgrams.IsChecked;
            bool ok = Decimal.TryParse(inBoxForce.inputBox.Text, out Decimal ss);
            if(ok)
            {
                if (cbnewton.IsChecked == true)
                {
                    inBoxForce.MaxValue = (Decimal.Parse(_mainwindow.publicVars.MAX_VALUE_FORCE) /
                        _mainwindow.publicVars.FORCE_EXCHANGE_RATE).ToString("F2");
                    inBoxForce.inputBox.Text = (ss / _mainwindow.publicVars.FORCE_EXCHANGE_RATE).ToString("F2");
                }
                else
                {
                    inBoxForce.MaxValue = _mainwindow.publicVars.MAX_VALUE_FORCE;
                    inBoxForce.inputBox.Text = (ss * _mainwindow.publicVars.FORCE_EXCHANGE_RATE).ToString("F2");
                }
            }
        }

        private void btnDistanceSetOrigin_Click(object sender, RoutedEventArgs e)
        {
            //_mainwindow.viewModel.lb_Current_Distance = "0.00";
            _mainwindow.serialCommunication.myPort.WriteLine(
                _mainwindow.publicVars.HOST_CMD_RESET_MOTOR_POSITION.ToString());
        }

        private void btnForceSetOrigin_Click(object sender, RoutedEventArgs e)
        {
            _mainwindow.serialCommunication.myPort.WriteLine(
                _mainwindow.publicVars.HOST_CMD_RESET_LOAD_SENSOR.ToString());
        }
    }
}
