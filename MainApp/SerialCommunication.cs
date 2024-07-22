using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FiberPullStrain
{
    public class SerialCommunication
    {
        public event EventHandler<string> DataReceived; 
        public SerialPort myPort;

        public bool handshakesucceed = false;

        public SerialCommunication()
        {
            myPort = new SerialPort();
            //myPort.DataReceived += MyPort_DataReceived;
        }

        public async Task SearchAllCOMports()
        {
            var ports = new List<string>();
            ports.AddRange(SerialPort.GetPortNames());
            foreach (string port in ports)
            {
                if (myPort.IsOpen)
                {
                    try
                    {
                        myPort.Close();
                        myPort.DataReceived -= MyPort_DataReceived;
                        myPort.PortName = port;
                        myPort.BaudRate = 115200;
                        myPort.Open();
                    }
                    catch (Exception err)
                    {
                        DataReceived?.Invoke(this, err.Message);
                    }
                }
                else
                {
                    try
                    {
                        myPort.PortName = port;
                        myPort.BaudRate = 115200;
                        myPort.DataReceived -= MyPort_DataReceived;
                        myPort.Open();
                    }
                    catch (Exception err)
                    {
                       DataReceived?.Invoke(this,err.Message);
                    }

                }

                if (myPort.IsOpen)
                {
                    try
                    {
                        int i = 0;
                        while (!handshakesucceed && i < 3)
                        {
                            if(i==2) // reset serial port.
                            {
                                myPort.DtrEnable = true;
                                DataReceived?.Invoke(this, $"Trying to reset {myPort.PortName}, please wait...");
                                await Task.Delay(50);
                                myPort.DtrEnable = false;
                                DataReceived?.Invoke(this, $"Reset {myPort.PortName} in processing, please wait...");
                                await Task.Delay(50);
                            }
                            myPort.DiscardOutBuffer();
                            //myPort.DiscardInBuffer();
                            //await Task.Delay(200);
                            myPort.WriteLine("h");
                            var stattime = DateTime.UtcNow;
                            while (myPort.BytesToRead <= 0) // every try wait 1 second...
                            {
                                if ((DateTime.UtcNow - stattime).TotalMilliseconds > 1000) break;
                            }
                            if (myPort.BytesToRead > 0)
                            {
                                string ss = myPort.ReadExisting();
                                if (ss.Contains("FiberPull"))
                                {
                                    handshakesucceed = true;
                                    DataReceived?.Invoke(this, $"Handshaking Succeed with {myPort.PortName}");
                                    myPort.DiscardInBuffer();
                                    myPort.DiscardOutBuffer();
                                    myPort.DataReceived += MyPort_DataReceived;
                                    return;
                                }
                                else
                                {
                                    myPort.DiscardInBuffer();
                                    myPort.DiscardOutBuffer();
                                    
                                    DataReceived?.Invoke(this,$"Hand shaking with {myPort.PortName} failed...");
                                }

                            }
                            i++;
                            DataReceived?.Invoke(this, $"Trying Hand shaking with {myPort.PortName} ... {i.ToString()}");
                        }
                        if (!handshakesucceed)
                        {
                            myPort.Close();
                            DataReceived?.Invoke(this, $"No instrument found on {myPort.PortName}."); 
                        }

                    }
                    catch (Exception err)
                    {
                        DataReceived?.Invoke(this, err.Message);
                    }
                }
            }
        }
        public void MyPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            string data = myPort.ReadLine().TrimEnd();
            DataReceived?.Invoke(this, $"Data incoming...{data}");
            if (data.Length > 0)
            {
                if (!handshakesucceed)
                {
                    if (data.Contains("FiberPull")) // invoke UI information update function
                    {
                        DataReceived?.Invoke(this, $"{myPort.PortName} -- Hand Shaking succeed.");
                        handshakesucceed = true;
                    }
                    else
                    {
                        handshakesucceed = false;
                        myPort.Close();
                        DataReceived?.Invoke(this, $"{myPort.PortName} -- Hand Shaking failed.");
                    }
                }

                else
                {
                    DataReceived?.Invoke(this, data);//????????????????????????????????
                }
            }
        }
        public void SimulateDataReceived(string data)
        {
            DataReceived?.Invoke(this, data);
        }
    }

}
