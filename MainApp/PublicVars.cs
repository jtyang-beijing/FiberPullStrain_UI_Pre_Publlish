using FiberPullStrain.CustomControl.view;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace FiberPullStrain
{
    public partial class PublicVars: ViewModelBase
    {
        public Decimal DISTANCE_EXCHANGE_RATE;
        public Decimal FORCE_EXCHANGE_RATE;
        public Decimal MOTOR_SCALE;

        public string CURRENT_DISTANCE;
        public string CURRENT_FORCE;
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

        public int LAST_SERIES_ID;
        public Color4 LAST_COLOR;

        public bool HANDSHAKESUCCEED;

        //public List<string> IN_BUFFER;
        public ConcurrentQueue<string> IN_BUFFER { get; private set; }

        public PublicVars()
        {
            DISTANCE_EXCHANGE_RATE = (Decimal)2.54;
            FORCE_EXCHANGE_RATE = (Decimal)101.971621;
            // Custermizable public varialbles
            MOTOR_SCALE = (Decimal)909.09090909; //steps per mm
            MAX_VALUE_DISTANCE = "26"; 
            MAX_VALUE_FORCE = "5000";
            //----------------------------------------
            CURRENT_DISTANCE = "0.00";
            CURRENT_FORCE = "0.00";
            CURRENT_CURVE_SERIES = 0;
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

            HANDSHAKESUCCEED = false;

            IN_BUFFER = new ConcurrentQueue<string>();
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
    }
}
