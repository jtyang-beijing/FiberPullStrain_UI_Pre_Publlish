using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FiberPullStrain.MainView
{
    internal class MainViewModel : INotifyPropertyChanged
    {

        public MainViewModel()
        {
            SerialCommunication serialCommunication = new SerialCommunication();
			serialCommunication.DataReceived += OnDataReceived;
        }

        private void OnDataReceived(object sender, string data)
        {
            string[] str = data.Split(':');
            if(str.Length>0)
            {
                if (str[0] == "d") SerialDistance = str[1];
                else if (str[0] == "f") SerialForce = str[1];
                else SerialInfo = str[1];
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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
    }
}
