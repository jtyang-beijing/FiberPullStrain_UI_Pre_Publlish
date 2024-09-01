using FiberPull;
using FiberPullStrain.CustomControl.view;
using GLGraphs.CartesianGraph;
using Microsoft.Win32;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Windows;

namespace FiberPullStrain
{
    public partial class PublicVars: ViewModelBase
    {
        public Decimal DISTANCE_EXCHANGE_RATE;
        public Decimal FORCE_EXCHANGE_RATE;
        public Decimal MOTOR_SCALE;

        public string CURRENT_DISTANCE;
        public string CURRENT_FORCE;
        public string DESTINATION;
        public string TARGET_FORCE;
        public string MOTOR_SPEED;
        public string MOTOR_ACCELORATION;
        public int CURRENT_CURVE_SERIES;

        public char HOST_CMD_STOP_MOTOR;
        public char HOST_CMD_RESET_MOTOR_POSITION;
        public char HOST_CMD_RESET_LOAD_SENSOR;
        public char HOST_CMD_CALIBRATE_LOAD_SENSOR;
        public char HOST_CMD_HANDSHAKE;
        public char HOST_CMD_GET_CURRENT_MOTOR_POSITION;
        public char HOST_CMD_SET_LOADSENSOR_CAL_FACTOR;
        public char HOST_CMD_DRIVE_MOTOR;
        public char HOST_CMD_HANDSHAKE_CONFIRM;
        public char HOST_CMD_EXIT_FIRMWARE;
        public char HOST_CMD_SET_MOTOR_SPEED;
        public char HOST_CMD_SET_MOTOR_ACCELORATION;

        public int LAST_SERIES_ID;
        public Color4 LAST_COLOR;

        public bool HANDSHAKESUCCEED;
        public bool MOVE_FORWARD;

        public GraphSeries<string> SERIES;
        public Dictionary<string, int> CURVE_SERIES;
        public bool LINE_SERIES;

        //public List<string> IN_BUFFER;
        public ConcurrentQueue<string> IN_BUFFER { get; private set; }

        private MainWindow _mainWindow { set;get; }
        public PublicVars()
        {
            _mainWindow = Application.Current.MainWindow as MainWindow;
            DISTANCE_EXCHANGE_RATE = (Decimal)2.54;
            FORCE_EXCHANGE_RATE = (Decimal)101.971621;
            // Custermizable public varialbles
            MOTOR_SCALE = (Decimal)909.09090909; //steps per mm
            MAX_VALUE_DISTANCE = "26"; 
            MAX_VALUE_FORCE = "5000";
            DESTINATION = "0.00";
            TARGET_FORCE = "0.00";
            MOTOR_SPEED = "100";
            MOTOR_ACCELORATION = "100";
        //----------------------------------------
            CURRENT_DISTANCE = "0.00";
            CURRENT_FORCE = "0.00";
            CURRENT_CURVE_SERIES = -1;
            LAST_SERIES_ID = -1;
            HOST_CMD_STOP_MOTOR = 'e';
            HOST_CMD_RESET_MOTOR_POSITION = 'o';
            HOST_CMD_RESET_LOAD_SENSOR = 't';
            HOST_CMD_CALIBRATE_LOAD_SENSOR = 'c';
            HOST_CMD_HANDSHAKE = 'h';
            HOST_CMD_GET_CURRENT_MOTOR_POSITION = 'p';
            HOST_CMD_SET_LOADSENSOR_CAL_FACTOR = 'r';
            HOST_CMD_DRIVE_MOTOR = 'm';
            HOST_CMD_HANDSHAKE_CONFIRM = 'g';
            HOST_CMD_EXIT_FIRMWARE = 'x';
            HOST_CMD_SET_MOTOR_SPEED = 's';
            HOST_CMD_SET_MOTOR_ACCELORATION = 'a';

            HANDSHAKESUCCEED = false;
            MOVE_FORWARD = true;

            IN_BUFFER = new ConcurrentQueue<string>();
            SERIES = null;
            CURVE_SERIES = new Dictionary<string, int>();
            LINE_SERIES = true;
        }
        private string max_value_distance;

        public string MAX_VALUE_DISTANCE
        {
            get { return max_value_distance; }
            set 
            { 
                max_value_distance = value;
                OnPropertyChanged();
            }
        }

        private string max_value_force;

        public string MAX_VALUE_FORCE
        {
            get { return max_value_force; }
            set 
            { 
                max_value_force = value;
                OnPropertyChanged();
            }
        }
        //-------------------------
        public void set_motor_acceloration(string acceloration)
        {
            try
            {
                if (_mainWindow.serialCommunication.myPort.IsOpen)
                {
                    _mainWindow.serialCommunication.myPort.Write
                        (_mainWindow.publicVars.HOST_CMD_SET_MOTOR_ACCELORATION.ToString() +
                        acceloration + '\n');
                    //MessageBox.Show("Motor Acceloration changed.", "Warnning",
                    //    MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public void set_motor_speed(string speed)
        {
            try
            {
                if (_mainWindow.serialCommunication.myPort.IsOpen)
                {
                    _mainWindow.serialCommunication.myPort.Write
                        (_mainWindow.publicVars.HOST_CMD_SET_MOTOR_SPEED.ToString() +
                        speed + '\n');
                    //MessageBox.Show("Motor moving speed changed.", "Warnning",
                    //    MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public void set_motor_scale(string scale)
        {
            MOTOR_SCALE = Decimal.Parse(scale);
        }
        public void set_MaxValues(string maxDistance, string maxForce)
        {
            _mainWindow.myButtonControls.inBoxDistance.MaxValue = maxDistance;
            _mainWindow.myButtonControls.inBoxDistance.MinValue = "-" + maxDistance;
            _mainWindow.myButtonControls.inBoxForce.MaxValue = maxForce;
        }

        public string read_from_registry(string keyName, string itemName, string defaultValue)
        {
            try
            {
                using (RegistryKey _key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\QuantumTech\FiberPull\" + keyName, false))
                {
                    if (_key == null)
                    {
                        return defaultValue;
                    }
                    return _key.GetValue(itemName, defaultValue)?.ToString() ?? defaultValue;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool write_to_registry(string keyName, string itemName, string keyValue)
        {
            try
            {
                RegistryKey _key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\QuantumTech\FiberPull\" + keyName);
                _key.SetValue(itemName, keyValue);
                return true;
            }
            catch (Exception exp)
            {

                return false;
            }

        }
        //-----------------------------
    }
}
