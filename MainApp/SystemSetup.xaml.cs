﻿using FiberPull;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FiberPullStrain
{
    /// <summary>
    /// Interaction logic for SystemSetup.xaml
    /// </summary>
    public partial class SystemSetup : Window
    {
        private float start_p, end_p, _distance;

        private MainWindow _mainWindow { get; set; }
        public SystemSetup()
        {
            InitializeComponent();
            _mainWindow = Application.Current.MainWindow as MainWindow;
        }

        private void calibrate_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res =
            MessageBox.Show("Following these steps to Run Force Sensor Calibration:\n\n" +
                "1. Attach a Known load onto the stage sensor\n" +
                "2. Input real load in Grams into text box\n" +
                "3. Press Finish Button.\n\n" +
                "Please check informations from Bottom of Main Winodw\n" +
                "during operation.", "Information", MessageBoxButton.OKCancel,
                MessageBoxImage.Information);
            if (res == MessageBoxResult.OK)
            {
                calibrate_load_cell();
                real_weight.IsEnabled = true;
                real_weight.Focus();
                real_weight.SelectAll();
                finish.IsEnabled = true;
            }
            else { }
        }
        private void calibrate_load_cell()
        {
            if (_mainWindow.serialCommunication.myPort.IsOpen)
            {
                try
                {
                    _mainWindow.serialCommunication.myPort.WriteLine
                        (_mainWindow.publicVars.HOST_CMD_CALIBRATE_LOAD_SENSOR.ToString());
                    MessageBox.Show("Now attach load onto stage sensor and then continue...\n\n" +
                        "DO NOT REMOVE LOAD Until input real load and press Finish Button.",
                        "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception err)
                {
                    MessageBox.Show($"Error: {err.Message}");
                }
            }
        }

        private void save_calibrate()
        {
            if (_mainWindow.serialCommunication.myPort.IsOpen)
            {
                try
                {
                    _mainWindow.serialCommunication.myPort.WriteLine(real_weight.Text);
                    Thread.Sleep(1000);
                    _mainWindow.serialCommunication.myPort.Write("y");
                }
                catch (Exception err)
                {
                    MessageBox.Show($"Error: {err.Message}");
                }
            }
        }

        private void cancle_calibrate()
        {
            if (_mainWindow.serialCommunication.myPort.IsOpen)
            {
                try
                {
                    _mainWindow.serialCommunication.myPort.WriteLine("n");
                }
                catch (Exception err)
                {
                    MessageBox.Show($"Error: {err.Message}");
                }
            }
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res =
            MessageBox.Show("Are you Sure to save the calibration?",
            "Information", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (res == MessageBoxResult.OK)
            {
                save_calibrate();
                MessageBox.Show("Calibration Succeed.", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                cancle_calibrate();
                MessageBox.Show("Calibration was not stored.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void end_position_TextChanged(object sender, TextChangedEventArgs e)
        {
            float.TryParse(end_position.Text, out end_p);
            if (end_p > start_p) save_cal.IsEnabled = true;
        }

        private void saveMotorProperty_Click(object sender, RoutedEventArgs e)
        {
            float.TryParse(setMotorSpeed.Text, out float _speed);
            float.TryParse(setAcceloration.Text, out float _acc);
            if (_speed > 0 && _speed <= 100 && _acc > 0 && _acc <= 100)
            {
                _mainWindow.publicVars.set_motor_speed(setMotorSpeed.Text);
                Thread.Sleep(300);
                _mainWindow.publicVars.set_motor_acceloration(setAcceloration.Text);
                _mainWindow.publicVars.write_to_registry("Motor", "Speed", setMotorSpeed.Text);
                _mainWindow.publicVars.write_to_registry("Motor", "Acceloration", setAcceloration.Text);
                MessageBox.Show("Motor Speed and Acceloration are set to New Values.",
                "Warnning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else MessageBox.Show("Please input numbers between 0 to 100.", 
                "Warnning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void save_cal_Click(object sender, RoutedEventArgs e)
        {
            if (float.TryParse(end_position.Text, out end_p))
            {
                _distance = end_p - start_p;
                if (_distance > 0)
                {// change motor scale
                    _mainWindow.publicVars.set_motor_scale(((Decimal)(10000 / _distance)).ToString());
                    _mainWindow.publicVars.write_to_registry("Motor", "Scale", _mainWindow.publicVars.MOTOR_SCALE.ToString());
                    MessageBox.Show("Motor Calibration data saved.");
                }
                else
                {
                    MessageBox.Show("End positin must be bigger than start potition.");
                }
            }
            else
            {
                MessageBox.Show("Invalid end potition.");
            }
        }

        private void run_motor_Click(object sender, RoutedEventArgs e)
        {
            if (float.TryParse(start_position.Text, out start_p))
            {
                if (_mainWindow.serialCommunication.myPort.IsOpen)
                {
                    float.TryParse(_mainWindow.publicVars.CURRENT_DISTANCE, out float current_motor_position);
                    current_motor_position = current_motor_position * (float)_mainWindow.publicVars.MOTOR_SCALE;
                    string cmd = "m" + (current_motor_position + 10000).ToString("F0");
                    _mainWindow.serialCommunication.myPort.WriteLine(cmd); // drive motor 10000 steps

                    MessageBox.Show("Driving Motor to New position...\n\n" +
                        "When Motor Stops, Measure and input Current \n" +
                        "Motor position.", "Information",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    end_position.IsEnabled = true;
                    end_position.Focus();
                    end_position.SelectAll();
                }
            }
            else { MessageBox.Show("Invalid start potition."); }

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void exit_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void save_max_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainWindow.publicVars.set_MaxValues(max_distance.Text, max_force.Text);
                _mainWindow.publicVars.write_to_registry("Motor", "Max Distance", max_distance.Text);
                _mainWindow.publicVars.write_to_registry("Sensor", "Max Load", max_force.Text);
                MessageBox.Show("Motor Maximum running distance\nand Maximum load are set to new Values.",
                "Warnning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex) { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setMotorSpeed.Text = _mainWindow.publicVars.read_from_registry("Motor", "Speed","100");
            setAcceloration.Text = _mainWindow.publicVars.read_from_registry("Motor", "Acceloration", "100");
            max_distance.Text = _mainWindow.publicVars.read_from_registry("Motor", "Max Distance", "26");
            max_force.Text = _mainWindow.publicVars.read_from_registry("Sensor", "Max Load", "1000");
        }
    }
}
