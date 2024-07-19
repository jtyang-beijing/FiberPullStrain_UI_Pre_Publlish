using System.Windows;

namespace FiberPullStrain
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
