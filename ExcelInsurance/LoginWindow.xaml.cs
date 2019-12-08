using ExcelInsurance.Repository.Implementations;
using ExcelInsurance.Repository.Interfaces;
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
using System.Windows.Shapes;

namespace ExcelInsurance
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IAuthManager _authManager;
        public LoginWindow()
        {
            InitializeComponent();
            _authManager = new AuthManager();
        }

        private void Btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (!_authManager.ValidateUserLogin(txt_username.Text, txt_password.Password))
            {
                txt_username.BorderBrush = System.Windows.Media.Brushes.Red;
                txt_password.BorderBrush = System.Windows.Media.Brushes.Red;
                MessageBox.Show("Invalid username or password.", "Error");
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
