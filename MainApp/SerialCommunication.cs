using FiberPull;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;

namespace FiberPullStrain
{
    public class SerialCommunication
    {
        public event EventHandler<string> DataReceived; 
        public SerialPort myPort;
        private MainWindow _mainWindow {  get; set; }

        public SerialCommunication()
        {
            myPort = new SerialPort();
            _mainWindow = Application.Current.MainWindow as MainWindow;
        }
        public async Task SearchAllCOMports()
        {
            var ports = SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                try
                {
                    // Close the port if it's open
                    if (myPort.IsOpen)
                    {
                        myPort.Close();
                    }

                    // Configure the port
                    myPort.PortName = port;
                    myPort.BaudRate = 115200;
                    myPort.Open();

                    if (myPort.IsOpen)
                    {
                        int attempts = 0;
                        _mainWindow.publicVars.HANDSHAKESUCCEED = false;

                        while (!_mainWindow.publicVars.HANDSHAKESUCCEED && attempts < 3)
                        {
                            if (attempts == 2)
                            {
                                // Reset the serial port
                                myPort.DtrEnable = true;
                                DataReceived?.Invoke(this, $"Trying to reset {myPort.PortName}, please wait...");
                                await Task.Delay(50);
                                myPort.DtrEnable = false;
                                DataReceived?.Invoke(this, $"Reset {myPort.PortName} in process, please wait...");
                                await Task.Delay(50);
                            }

                            // Clear the output buffer and send the handshake command
                            myPort.DiscardOutBuffer();
                            myPort.WriteLine(_mainWindow.publicVars.HOST_CMD_HANDSHAKE.ToString());

                            // Wait for a response with a timeout
                            var startTime = DateTime.UtcNow;
                            while (myPort.BytesToRead <= 0)
                            {
                                if ((DateTime.UtcNow - startTime).TotalMilliseconds > 1000)
                                {
                                    break;
                                }
                            }

                            // Check if we received a response
                            if (myPort.BytesToRead > 0)
                            {
                                string response = myPort.ReadExisting();
                                if (response.Contains("Fiber"))
                                {
                                    _mainWindow.publicVars.HANDSHAKESUCCEED = true;
                                    DataReceived?.Invoke(this, $"Handshaking succeeded with {myPort.PortName}");

                                    // Clear buffers and attach the event handler
                                    myPort.DiscardInBuffer();
                                    myPort.DiscardOutBuffer();

                                    // Attach the event handler if not already attached
                                    myPort.DataReceived -= MyPort_DataReceived; // Ensure it's detached first
                                    myPort.DataReceived += MyPort_DataReceived;
                                    return;
                                }
                                else
                                {
                                    DataReceived?.Invoke(this, $"Handshake with {myPort.PortName} failed...");
                                }
                            }
                            else
                            {
                                DataReceived?.Invoke(this, $"No response on {myPort.PortName}...");
                            }

                            attempts++;
                            DataReceived?.Invoke(this, $"Attempting handshake with {myPort.PortName}... Attempt {attempts}");
                        }

                        // If handshake failed after 3 attempts
                        if (!_mainWindow.publicVars.HANDSHAKESUCCEED)
                        {
                            myPort.Close();
                            DataReceived?.Invoke(this, $"No instrument found on {myPort.PortName}.");
                        }
                    }
                }
                catch (Exception err)
                {
                    DataReceived?.Invoke(this, err.Message);
                }
            }
        }

        //public async Task SearchAllCOMports()
        //{
        //    var ports = new List<string>();
        //    ports.AddRange(SerialPort.GetPortNames());
        //    foreach (string port in ports)
        //    {
        //        if (myPort.IsOpen)
        //        {
        //            try
        //            {
        //                myPort.Close();
        //                myPort.PortName = port;
        //                myPort.BaudRate = 115200;
        //                myPort.Open();
        //            }
        //            catch (Exception err)
        //            {
        //                DataReceived?.Invoke(this, err.Message);
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                myPort.PortName = port;
        //                myPort.BaudRate = 115200;
        //                myPort.Open();
        //            }
        //            catch (Exception err)
        //            {
        //               DataReceived?.Invoke(this,err.Message);
        //            }
        //        }

        //        if (myPort.IsOpen)
        //        {
        //            try
        //            {
        //                int i = 0;
        //                while (!_mainWindow.publicVars.HANDSHAKESUCCEED && i < 3)
        //                {
        //                    if(i==2) // reset serial port.
        //                    {
        //                        myPort.DtrEnable = true;
        //                        DataReceived?.Invoke(this, $"Trying to reset {myPort.PortName}, please wait...");
        //                        await Task.Delay(50);
        //                        myPort.DtrEnable = false;
        //                        DataReceived?.Invoke(this, $"Reset {myPort.PortName} in processing, please wait...");
        //                        await Task.Delay(50);
        //                    }
        //                    myPort.DiscardOutBuffer();
        //                    //myPort.DiscardInBuffer();
        //                    //await Task.Delay(200);
        //                    myPort.WriteLine(_mainWindow.publicVars.HOST_CMD_HANDSHAKE.ToString());
        //                    var stattime = DateTime.UtcNow;
        //                    while (myPort.BytesToRead <= 0) // every try wait 1 second...
        //                    {
        //                        if ((DateTime.UtcNow - stattime).TotalMilliseconds > 1000) break;
        //                    }
        //                    if (myPort.BytesToRead > 0)
        //                    {
        //                        string ss = myPort.ReadExisting();
        //                        if (ss.Contains("Fiber"))
        //                        {
        //                            _mainWindow.publicVars.HANDSHAKESUCCEED = true;
        //                            DataReceived?.Invoke(this, $"Handshaking Succeed with {myPort.PortName}");
        //                            myPort.DiscardInBuffer();
        //                            myPort.DiscardOutBuffer();
        //                            myPort.DataReceived += MyPort_DataReceived;
        //                            return;
        //                        }
        //                        else
        //                        {
        //                            myPort.DiscardInBuffer();
        //                            myPort.DiscardOutBuffer();

        //                            DataReceived?.Invoke(this,$"Hand shaking with {myPort.PortName} failed...");
        //                        }
        //                    }
        //                    i++;
        //                    DataReceived?.Invoke(this, $"Trying Hand shaking with {myPort.PortName} ... {i.ToString()}");
        //                }
        //                if (!_mainWindow.publicVars.HANDSHAKESUCCEED)
        //                {
        //                    myPort.Close();
        //                    DataReceived?.Invoke(this, $"No instrument found on {myPort.PortName}."); 
        //                }

        //            }
        //            catch (Exception err)
        //            {
        //                DataReceived?.Invoke(this, err.Message);
        //            }
        //        }
        //    }
        //}
        public void MyPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            string data = myPort.ReadLine().TrimEnd();
            DataReceived?.Invoke(this, $"Data incoming...{data}");
            if (data.Length > 0)
            {
                if (!_mainWindow.publicVars.HANDSHAKESUCCEED)
                {
                    if (data.Contains("FiberPull")) // invoke UI information update function
                    {
                        DataReceived?.Invoke(this, $"{myPort.PortName} -- Hand Shaking succeed.");
                        _mainWindow.publicVars.HANDSHAKESUCCEED = true;
                    }
                    else
                    {
                        _mainWindow.publicVars.HANDSHAKESUCCEED = false;
                        myPort.Close();
                        DataReceived?.Invoke(this, $"{myPort.PortName} -- Hand Shaking failed.");
                    }
                }

                else
                {
                    DataReceived?.Invoke(this, data);
                }
            }
        }
        public void SimulateDataReceived(string data)
        {
            DataReceived?.Invoke(this, data);
        }
    }

}
