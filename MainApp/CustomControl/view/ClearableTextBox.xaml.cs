using FiberPull;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace FiberPullStrain.CustomControl.view
{
    public partial class ClearableTextBox : UserControl
    {
        public ClearableTextBox()
        {
            InitializeComponent();
            DataContext = MainWindow.DataContextProperty;
        }
        
        /*
         Serve for value range limitation
         */
        public string MinValue
        {
            get { return GetValue(MinValueProperty).ToString(); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(string), 
                typeof(ClearableTextBox), new PropertyMetadata("0"));// default value set to string "0"

        public string MaxValue
        {
            get { return GetValue(MaxValueProperty).ToString(); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(string), 
                typeof(ClearableTextBox), new PropertyMetadata("100"));// default value set to string "100"

        private void btnClear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            inputBox.Clear();
            inputBox.Focus();
        }

        private void inputBox_TextChanged(object sender, TextChangedEventArgs e)
        {   /* dealing with place holder text. show when no inputs to text box
             * hide when input detected.
            */
            if(string.IsNullOrEmpty(inputBox.Text))
            {
                tbPlaceHolder.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                tbPlaceHolder.Visibility = System.Windows.Visibility.Hidden;
                if (inputBox.Text !="-" && !float.TryParse(inputBox.Text, out float v))
                {
                    inputBox.Text = "Invalid";
                }
            }
            /*
             * dealing with value range limit.
             */
            if (float.TryParse(inputBox.Text, out float value))
            {
                if (value < float.Parse(MinValue))
                {
                    inputBox.Text = MinValue;
                }
                else if (value > float.Parse(MaxValue))
                {
                    inputBox.Text = MaxValue;
                }

                inputBox.SelectionStart = inputBox.Text.Length;
            }
        }


        private void inputBox_MouseEnter(object sender, MouseEventArgs e)
        {
            inputBox.Focus();
            tbPlaceHolder.Foreground = new SolidColorBrush(Colors.Orange);
        }

        private void inputBox_MouseLeave(object sender, MouseEventArgs e)
        {
            tbPlaceHolder.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }
}
