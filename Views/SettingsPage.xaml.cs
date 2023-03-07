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

namespace TiresShopApplication.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void SaveNewPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            switch (MainWindow.User)
            {
                case AuthenticationWindow.User.Admin:
                    if (OldPasswordTextbox.Text == AuthData.Default.adminPassword)
                    {
                        AuthData.Default.adminPassword = NewPasswordTextbox.Text;
                    }
                    else
                    {
                        MessageBox.Show("Старый пароль введён неверно!");
                        OldPasswordTextbox.Text = default(string);
                        NewPasswordTextbox.Text = default(string);
                        return;
                    }
                    break;
                case AuthenticationWindow.User.Manager:
                    if (OldPasswordTextbox.Text == AuthData.Default.managerPassword)
                    {
                        AuthData.Default.managerPassword = NewPasswordTextbox.Text;
                    }
                    else
                    {
                        MessageBox.Show("Старый пароль введён неверно!");
                        OldPasswordTextbox.Text = default(string);
                        NewPasswordTextbox.Text = default(string);
                        return;
                    }
                    break;
                default:
                    break;
            }
            OldPasswordTextbox.Text = default(string);
            NewPasswordTextbox.Text = default(string);
            AuthData.Default.Save();
            MessageBox.Show("Новый пароль успешно сохранен!");
        }
    }
}
