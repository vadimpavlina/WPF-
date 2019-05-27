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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace START
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MainWindow r = new MainWindow();
        public List<Person> people = new List<Person>();
      
        public Login()
        {
            InitializeComponent();
            lblName.Visibility = Visibility.Hidden;
            lblSur.Visibility = Visibility.Hidden;
            imgProf.Visibility = Visibility.Hidden;
            lblAge.Visibility = Visibility.Hidden;
            lblLogin.Visibility = Visibility.Hidden;
            //-------------------------------------
            listUser.Visibility = Visibility.Hidden;
           


        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {

            
            bool check = false;
            foreach (var item in people)
            {

                if (txtLogin.Text == item.Login && txtPaswd.Text == item.Paswd)
                {
                    lblName.Content = item.Name;
                    lblSur.Content = item.Surname;
                    lblLogin.Content = item.Login;
                    lblAge.Content = item.Age;
                    if (txtLogin.Text == "admin")
                    {
                        //imgProf.Source = new BitmapImage(new Uri(@"C:\Users\Vadim\Desktop\START\START\bin\Debug\admin.png"));
                        lblName.Visibility = Visibility.Hidden;
                        lblSur.Visibility = Visibility.Hidden;
                        imgProf.Visibility = Visibility.Hidden;
                        lblAge.Visibility = Visibility.Hidden;
                        lblLogin.Visibility = Visibility.Hidden;
                        //-------------------------------------
                        listUser.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        lblName.Visibility = Visibility.Visible;
                        lblSur.Visibility = Visibility.Visible;
                        imgProf.Visibility = Visibility.Visible;
                        lblAge.Visibility = Visibility.Visible;
                        lblLogin.Visibility = Visibility.Visible;
                        //-------------------------------------
                        listUser.Visibility = Visibility.Hidden;
                        //-------------------------------------
                        imgProf.Source = new BitmapImage(new Uri(item.PathImg));
                    }
                    check = true;
                    break;
                }
            }
            if (check == false)
            {
                MessageBox.Show("Cannot find user ");
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SerializeAndSave(@"C: \Users\Vadim\Desktop\START\START\Logger.txt", people);

            Process[] processes = Process.GetProcessesByName("START");
            foreach (var process in processes)
            {
                process.Kill();
            }
        }
        
        public void SerializeAndSave(string path, List<Person> data)
        {
            var serializer = new XmlSerializer(typeof(List<Person>));
            using (var writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, data);
            }
        }
        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            SerializeAndSave(@"C: \Users\Vadim\Desktop\START\START\Logger.txt", people);
            imgProf.Source = null;
            lblName.Content = null;
            lblSur.Content = null;
            lblAge.Content = null;
            lblLogin.Content = null;
            r.people = people;
            this.Hide();
            r.Show();
        }

        private void ListUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblName.Visibility = Visibility.Visible;
            lblSur.Visibility = Visibility.Visible;
            imgProf.Visibility = Visibility.Visible;
            lblAge.Visibility = Visibility.Visible;
            lblLogin.Visibility = Visibility.Visible;
            //----------------------------------------------
            imgProf.Source = null;
            lblName.Content = null;
            lblSur.Content = null;
            lblAge.Content = null;
            lblLogin.Content = null;
            //----------------------------------------------
            lblName.Content = people[listUser.SelectedIndex].Name;
            lblSur.Content = people[listUser.SelectedIndex].Surname;
            lblAge.Content = people[listUser.SelectedIndex].Age;
            lblLogin.Content = people[listUser.SelectedIndex].Login;
            if (people[listUser.SelectedIndex].Login == "admin")
            {
                imgProf.Source = new BitmapImage(new Uri(@"C:\Users\Vadim\Desktop\START\START\bin\Debug\admin.png"));
            }
            else
            {
                imgProf.Source = new BitmapImage(new Uri(people[listUser.SelectedIndex].PathImg));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in people)
            {
                listUser.Items.Add(item.Name + " " + item.Surname);
            }
        }
    }
}
