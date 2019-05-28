using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DataGridMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool check = true;
        ObservableCollection<User> users = new ObservableCollection<User>();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            lblId.Visibility = Visibility.Visible;
            lblImg.Visibility = Visibility.Visible;
            lblName.Visibility = Visibility.Visible;
            //====================================
            txtID.Visibility = Visibility.Visible;
            txtName.Visibility = Visibility.Visible;
            ImgUrl.Visibility = Visibility.Visible;
            //=======================================
            datePick.Visibility = Visibility.Visible;
            //=====================================
            SaveUser.Visibility = Visibility;
            check = true;
        }

        private void BtnChangeUser_Click(object sender, RoutedEventArgs e)
        {

            lblId.Visibility = Visibility.Visible;
            lblImg.Visibility = Visibility.Visible;
            lblName.Visibility = Visibility.Visible;
            //====================================
            txtID.Visibility = Visibility.Visible;
            txtName.Visibility = Visibility.Visible;
            ImgUrl.Visibility = Visibility.Visible;
            //=======================================
            datePick.Visibility = Visibility.Visible;

            //=========================================
            btnAddUser.IsEnabled = false;
            //==============================================
            var userView = (dgSimple.SelectedItem as User);
            //=============================================
            txtID.Text = userView.Id.ToString();
            txtName.Text = userView.Name;
            ImgUrl.Text = userView.ImageUrl;
            datePick.SelectedDate = userView.Birthday;
            SaveUser.Visibility = Visibility.Visible;
            check = false;
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgSimple.SelectedItem != null)
                users.Remove(dgSimple.SelectedItem as User);
        }

        private void DgSimple_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            if (check == false)
            {
                if (dgSimple.SelectedItem != null)
                    users.Remove(dgSimple.SelectedItem as User);
            }
            //==================================
            users.Add(new User()
            {
                Id = Convert.ToInt32(txtID.Text),
                Name = txtName.Text,
                Birthday = datePick.SelectedDate.Value.Date,
                ImageUrl = ImgUrl.Text
            });
            //======================================
            txtID.Text = null;
            txtName.Text = null;
            ImgUrl.Text = null;
            datePick.SelectedDate = null;
            //=================
            SaveUser.Visibility = Visibility.Hidden;
            //======================================
            btnAddUser.IsEnabled = true;
            //======================================
            lblId.Visibility = Visibility.Hidden;
            lblImg.Visibility = Visibility.Hidden;
            lblName.Visibility = Visibility.Hidden;
            //====================================
            txtID.Visibility = Visibility.Hidden;
            txtName.Visibility = Visibility.Hidden;
            ImgUrl.Visibility = Visibility.Hidden;
            //=======================================
            datePick.Visibility = Visibility.Hidden;
            
        }
        //=======================================================================
        public void SerializeAndSave(string path, ObservableCollection<User> data)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<User>));
            using (var writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, data);
            }
        }
        //=======================================================================
        public ObservableCollection<User> ReadAndDeserialize(string path)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<User>));
            using (var reader = new StreamReader(@"C:\Users\Pavl_gm1b\Desktop\Work\WPF-\SimpleMVVM\DataGridMVVM\Person.txt"))
            {
                return (ObservableCollection<User>)serializer.Deserialize(reader);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SerializeAndSave(@"C:\Users\Pavl_gm1b\Desktop\Work\WPF-\SimpleMVVM\DataGridMVVM\Person.txt", users);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            users = ReadAndDeserialize(@"C:\Users\Pavl_gm1b\Desktop\Work\WPF-\SimpleMVVM\DataGridMVVM\Person.txt");
            dgSimple.ItemsSource = users;
        }
    }
    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name;
        private DateTime birthday;
        public DateTime Birthday
        {
            get { return this.birthday; }
            set
            {
                if (this.birthday != value)
                {
                    this.birthday = value;
                    this.NotifyPropertyChanged("Birthday");
                }
            }
        }
        public string ImageUrl { get; set; }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
