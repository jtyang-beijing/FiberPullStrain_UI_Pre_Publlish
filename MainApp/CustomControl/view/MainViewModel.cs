using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace FiberPullStrain.CustomControl.view
{
    public class MainViewModel : ViewModelBase
    {
        private protected readonly SerialCommunication _serialcommunication;
        private Dispatcher _dispatcher;
        private PublicVars _publicVars;
        public MainViewModel(SerialCommunication serialCommunication)
        {
            _publicVars = new PublicVars();
            _dispatcher = Dispatcher.CurrentDispatcher;
            _serialcommunication = serialCommunication;
            _serialcommunication.DataReceived += _serialcommunication_DataReceived;
            //_serialcommunication.SimulateDataReceived("f:0f0f");
            //_serialcommunication.SimulateDataReceived("d:0d0d");
            _serialcommunication.SimulateDataReceived("Searching Instrument, Please Wait...");
        }

        private void _serialcommunication_DataReceived(object sender, string e)
        {
            string[] str = e.Split(':');
            _dispatcher.Invoke(() =>
            {
                if (str[0] == "f")
                {
                    lb_Current_Force = str[1];
                }
                else if (str[0] == "d")
                {
                    string dd = (Decimal.Parse(str[1]) / _publicVars.MOTOR_SCALE).ToString("F2");
                    lb_Current_Distance = dd;
                }
                else
                {
                    Bar_Infor = e;
                }
            });

        }

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged();
            }
        }

        private string lb_current_distance;

        public string lb_Current_Distance
        {
            get { return lb_current_distance; }
            set
            {
                lb_current_distance = value;
                OnPropertyChanged();
                Onlb_Current_Distance_Changed();
            }
        }
        public event EventHandler lb_Current_Distance_Content_Changed;
        protected virtual void Onlb_Current_Distance_Changed()
        {
            lb_Current_Distance_Content_Changed?.Invoke(this, EventArgs.Empty);
        }

        private string lb_current_force;

        public string lb_Current_Force
        {
            get { return lb_current_force; }
            set
            {
                lb_current_force = value;
                OnPropertyChanged();
            }
        }

        private string bar_infor;

        public string Bar_Infor
        {
            get { return bar_infor; }
            set
            {
                bar_infor = value;
                OnPropertyChanged();
            }
        }

        //-------------------------------------------------------
        // bount text for text block - place holder text
        private string tblbountText;
        public string tblBoundText
        {
            get { return tblbountText; }
            set
            {
                tblbountText = value;
                OnPropertyChanged();
            }
        }
        // bount text for text input box
        private string tbbountText;
        public string tbBoundText
        {
            get { return tbbountText; }
            set
            {
                tbbountText = value;
                OnPropertyChanged();
            }
        }
    }
}