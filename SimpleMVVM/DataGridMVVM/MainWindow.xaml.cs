using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DataGridMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<User> users = new ObservableCollection<User>();
        public MainWindow()
        {
            InitializeComponent();
            users.Add(new User()
            {
                Id = 1,
                Name = "John Doe",
                Birthday = new DateTime(1971, 7, 23),
                ImageUrl = "http://www.hawthorngroup.com/wp-content/uploads/2019/03/john-1-300x300.jpg"
            });
            users.Add(new User()
            {
                Id = 2,
                Name = "Jane Doe",
                Birthday = new DateTime(1974, 1, 17),
                ImageUrl = "https://i.vimeocdn.com/portrait/7224366_300x300"
            });
            users.Add(new User()
            {
                Id = 3,
                Name = "Sammy Doe",
                Birthday = new DateTime(1991, 9, 2),
                ImageUrl = "https://sharerice.com/images/thumb/4/40/10931180_947171611960525_5370449655645132591_n.jpg/300px-10931180_947171611960525_5370449655645132591_n.jpg"
            });

            dgSimple.ItemsSource = users;
        }
   
        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtID.Text != "" && txtName.Text != "" && ImgUrl.Text != "")
            {
                users.Add(new User()
                {
                    Id = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Birthday = new DateTime(1991, 9, 2),
                    ImageUrl = ImgUrl.Text
                });
                txtID.Text = null;
                txtName.Text = null;
                ImgUrl.Text = null;
            }
          
        }
        
        private void BtnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            var userView = (dgSimple.SelectedItem as User);
            txtName.Text = userView.Name;
            txtID.Text = userView.Id.ToString();
            ImgUrl.Text = userView.ImageUrl;
            SaveUser.Visibility = Visibility.Visible;
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
            var userView = (dgSimple.SelectedItem as User);
            userView.Name = txtName.Text;
            userView.Id = Convert.ToInt32(txtID.Text);
            userView.ImageUrl = ImgUrl.Text;
            SaveUser.Visibility = Visibility.Hidden;
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
