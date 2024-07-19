using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace FiberPullStrain.CustomControl.view
{
    public class UserControlViewModel : INotifyPropertyChanged
    {
        private readonly Dispatcher _dispatcher;
        public event PropertyChangedEventHandler PropertyChanged;
        protected readonly SerialCommunication _serialCommunication;
        public UserControlViewModel(SerialCommunication serialCommunication)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            // Initialize the SerialCommunication and subscribe to its event
            _serialCommunication = serialCommunication;
            _serialCommunication.DataReceived += OnDataReceived;
            //_serialCommunication.SimulateDataReceived("f001");
            //_serialCommunication.SimulateDataReceived("d002");
            
        }
        private void OnDataReceived(object sender, string data)
        {
            string[] str = data.Split(":");
            _dispatcher.Invoke(() =>
            {
                if (str[0] == "f")
                {
                    SerialForce = str[1]; // Strip the prefix and update Serial_Infor
                }
                else if (str[0] == "d")
                {
                    SerialDistance = str[1]; // Strip the prefix and update Additional_Infor
                }
                else SerialInfo = str[0];
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
        private string serialinfo;
        public string SerialInfo
        {
            get { return serialinfo; }
            set
            {
                serialinfo = value;
                OnPropertyChanged();
            }
        }

        private string serialdistance;
        public string SerialDistance
        {
            get { return serialdistance; }
            set
            {
                serialdistance = value;
                OnPropertyChanged();
            }
        }

        private string serialforce;
        public string SerialForce
        {
            get { return serialforce; }
            set
            {
                serialforce = value;
                OnPropertyChanged();
            }
        }

        /* method to execute when setter of a control setting new value
 * 
 */
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
