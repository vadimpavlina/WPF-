using System.Windows;

namespace MenuAnimado1
{

    public partial class Logger : Window
    {
        public Logger()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Register registerWin = new Register();
            this.Hide();
            registerWin.ShowDialog();
        }
    }
}
