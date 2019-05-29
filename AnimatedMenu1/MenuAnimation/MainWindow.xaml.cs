using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MenuAnimado1
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void WinMain_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeBack();
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            ChangeBack();
        }
        void ChangeBack()
        {
            Random r = new Random();
            int bc = r.Next(2, 7);
            switch (bc)
            {

                case 2:
                    {
                        ImageBrush myBrush = new ImageBrush();

                        myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Assets/back2.jpg"));

                        this.Background = myBrush;
                        break;
                    }
                case 3:
                    {
                        ImageBrush myBrush = new ImageBrush();

                        myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Assets/back3.jpg"));

                        this.Background = myBrush;
                        break;
                    }
                case 4:
                    {
                        ImageBrush myBrush = new ImageBrush();

                        myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Assets/back4.jpg"));

                        this.Background = myBrush;
                        break;
                    }
                case 5:
                    {
                        ImageBrush myBrush = new ImageBrush();

                        myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Assets/back5.jpg"));

                        this.Background = myBrush;
                        break;
                    }
                case 6:
                    {
                        ImageBrush myBrush = new ImageBrush();

                        myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Assets/back6.jpg"));

                        this.Background = myBrush;
                        break;
                    }
                case 7:
                    {
                        ImageBrush myBrush = new ImageBrush();

                        myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Assets/back7.jpg"));

                        this.Background = myBrush;
                        break;
                    }
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush myBrush = new ImageBrush();

            myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "https://images.pexels.com/photos/2229732/pexels-photo-2229732.jpeg?cs=srgb&dl=agriculture-color-cropland-2229732.jpg&fm=jpg"));

            buttonProfil.Background = myBrush;
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


    }
}
