using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
using TiresShopApplication;

namespace TiresShopApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public AuthenticationWindow.User User { get; set; }
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletreHelper = new PaletteHelper();
        public MainWindow()
        {
            ITheme theme = paletreHelper.GetTheme();
            User = new AuthenticationWindow().ShowDialog();
            InitializeComponent();
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            if (User != AuthenticationWindow.User.Admin)
            {
                AdminPanelStackPanel.Visibility = Visibility.Hidden;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FullViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SearchTiresButton_Click(object sender, RoutedEventArgs e)
        {            
            MainFrame.Navigate(new Uri("/Views/SearchTiresPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void SearchWheelsButton_Click(object sender, RoutedEventArgs e)
        {          
            MainFrame.Navigate(new Uri("/Views/SearchWheelPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("/Views/SettingsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void AdminPanelButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("/Views/AdminPanelPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("/Views/CartPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
