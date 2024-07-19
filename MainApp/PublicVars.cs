using FiberPullStrain.CustomControl.view;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FiberPullStrain
{
    public partial class PublicVars: ViewModelBase
    {
        public Decimal DISTANCE_EXCHANGE_RATE = (Decimal)2.54;
        public Decimal FORCE_EXCHANGE_RATE = (Decimal)101.971621;
        public Decimal MOTOR_SCALE = (Decimal)909.09090909;

        public string CURRENT_DISTANCE;
        public string CURRENT_FORCE;
        public char HOST_CMD_STOP_MOTOR;
        public char HOST_CMD_RESET_MOTOR_POSITION;
        public char HOST_CMD_RESET_LOAD_SENSOR;

        public PublicVars()
        {
            MAX_VALUE_DISTANCE = "50"; 
            MAX_VALUE_FORCE = "5000";
            CURRENT_DISTANCE = "0.00";
            CURRENT_FORCE = "0.00";
            HOST_CMD_STOP_MOTOR = 'e';
            HOST_CMD_RESET_MOTOR_POSITION = 'o';
            HOST_CMD_RESET_LOAD_SENSOR = 't';
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
