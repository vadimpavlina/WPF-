using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace START
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Person> people = new List<Person>();
        string path;

        public MainWindow()
        {
            InitializeComponent();
           // people.Add(new Person { Age = "16", Surname = "System", Name = "Administrator", Login = "admin", Paswd = "12345678", PathImg = " " });
        }

        private void TxtName_TextChange(object sender, TextChangedEventArgs e)
        {
            var check = txtName.Text;
            for (int i = 0; i < check.Length; i++)
            {
                if (check[i] >= '0' && check[i] <= '9')
                {
                    txtName.Background = Brushes.Red;
                    btnConfirm.IsEnabled = false;
                }
                else
                {
                    txtName.Background = Brushes.LightGreen;
                    btnConfirm.IsEnabled = true;
                }
            }
            if (txtName.Text!=null)
            {
                btnConfirm.IsEnabled = true;
            }
        }

        private void TxtSurname_TextChange(object sender, TextChangedEventArgs e)
        {
            var check = txtSurname.Text;
            for (int i = 0; i < check.Length; i++)
            {
                if (check[i] >= '0' && check[i] <= '9')
                {
                    txtSurname.Background = Brushes.Red;
                    btnConfirm.IsEnabled = false;
                }
                else
                {
                    txtSurname.Background = Brushes.LightGreen;
                    btnConfirm.IsEnabled = true;
                }
            }
            if (txtSurname.Text != null)
            {
                btnConfirm.IsEnabled = true;
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgProfil.Source = new BitmapImage(new Uri(op.FileName));
                path = op.FileName;
            }
            if (imgProfil.Source != null)
            {
                btnConfirm.IsEnabled = true;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblAge.Content = (int)sliderAge.Value;
            if (sliderAge.Value != 0)
            {
                btnConfirm.IsEnabled = true;
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == null || txtName.Text == null || txtPasswd.Password.ToString() == null || txtSurname.Text == null || sliderAge.Value == 0||imgProfil.Source==null)
            {
                btnConfirm.IsEnabled = false;
            }
            else if (txtLogin.Text == "admin")
            {
                txtLogin.Background = Brushes.Red;
            }
            else
            {
                people.Add(new Person { Age = lblAge.Content.ToString(), Surname = txtSurname.Text, Name = txtName.Text, Login = txtLogin.Text, Paswd = txtPasswd.Password.ToString(), PathImg = path });
                MessageBox.Show(people.Count.ToString());
                txtLogin.Text = null;
                txtName.Text = null;
                txtPasswd.Password = null;
                txtSurname.Text = null;
                imgProfil.Source = null;
                sliderAge.Value = 0;
                Login loginWin = new Login();
                loginWin.people = people;
                this.Hide();
                loginWin.ShowDialog();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("START");
            foreach (var process in processes)
            {
                process.Kill();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          people = ReadAndDeserialize(@"C:\Users\Vadim\Desktop\START\START\Logger.txt");
        }
        public List<Person> ReadAndDeserialize(string path)
        {
            var serializer = new XmlSerializer(typeof(List<Person>));
            using (var reader = new StreamReader(@"C:\Users\Vadim\Desktop\START\START\Logger.txt"))
            {
                return (List<Person>)serializer.Deserialize(reader);
            }
        }
    }
}
