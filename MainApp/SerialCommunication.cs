﻿using FiberPull;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FiberPullStrain
{
    public class SerialCommunication
    {
        public event EventHandler<string> DataReceived; 
        public SerialPort myPort;
        private MainWindow _mainWindow {  get; set; }
        private SystemSetup systemSetup { get; set; }

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
               await HandShakeWithPort(port);
                if (_mainWindow.publicVars.HANDSHAKESUCCEED) return;
            }
        }

        public async Task HandShakeWithPort(string portname)
        {
            if (myPort.IsOpen)
            {
                try
                {
                    // Close the port if it's open
                    myPort.DiscardInBuffer();
                    myPort.DiscardOutBuffer();
                    myPort.Close();
                }
                catch (Exception err)
                {
                    DataReceived?.Invoke(this, err.Message);
                }
            }
            try
            {
                myPort.PortName = portname;
                myPort.BaudRate = 115200;
                myPort.Parity = Parity.None;
                myPort.StopBits = StopBits.One; // Ensure stop bits match your Arduino settings
                myPort.DataBits = 8; // Ensure data bits match your Arduino settings
                myPort.Handshake = Handshake.None; // Ensure handshake settings match your Arduino settings
                myPort.ReadTimeout = 1000; // Set read timeout
                myPort.WriteTimeout = 1000; // Set write timeout
                myPort.Open();
                myPort.DataReceived -= MyPort_DataReceived;
                myPort.DiscardInBuffer();
                myPort.DiscardOutBuffer();
                int attempts = 0;
                _mainWindow.publicVars.HANDSHAKESUCCEED = false;

                while (!_mainWindow.publicVars.HANDSHAKESUCCEED && attempts < 3)
                {
                    //if (attempts == 2)
                    //{
                    //    // Reset the serial port
                    //    myPort.DtrEnable = true;
                    //    DataReceived?.Invoke(this, $"Trying to reset {myPort.PortName}, please wait...");
                    //    await Task.Delay(100);
                    //    myPort.DtrEnable = false;
                    //    DataReceived?.Invoke(this, $"Reset {myPort.PortName} in process, please wait...");
                    //    await Task.Delay(100);
                    //}

                    // Send handshake command
                    myPort.Write(_mainWindow.publicVars.HOST_CMD_HANDSHAKE.ToString() + '\n');
                        
                    // Wait for a response with a timeout
                    var startTime = DateTime.UtcNow;
                    while (myPort.BytesToRead <= 0)
                    {// wait 2s. 1s is not enough to get responding...
                        if ((DateTime.UtcNow - startTime).TotalMilliseconds > 2000)
                        {
                            break;
                        }
                        await Task.Delay(10);
                    }

                    // Check if we received a response
                    if (myPort.BytesToRead > 0)
                    {
                        string response = myPort.ReadExisting();
                        if (response.Contains("Fiber"))
                        {
                            _mainWindow.publicVars.HANDSHAKESUCCEED = true;
                            DataReceived?.Invoke(this, $"Handshaking succeeded with {myPort.PortName}");
                            // Attach the event handler if not already attached
                            myPort.DataReceived -= MyPort_DataReceived; // Ensure it's detached first
                            myPort.DataReceived += MyPort_DataReceived;
                            myPort.Write(_mainWindow.publicVars.HOST_CMD_HANDSHAKE_CONFIRM.ToString() + '\n');
                            _mainWindow.publicVars.write_to_registry("COM Port", "Port", myPort.PortName);
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
            catch (Exception err)
            {
                DataReceived?.Invoke(this, err.Message);
            }
        }

        public void ClosePort()
        {
            if (myPort.IsOpen) { myPort.Close(); }
        }

        public void MyPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try 
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
            catch (Exception exp)
            { DataReceived?.Invoke(this, exp.Message); }
            

        }
        public void SimulateDataReceived(string data)
        {
            DataReceived?.Invoke(this, data);
        }
    }

}
