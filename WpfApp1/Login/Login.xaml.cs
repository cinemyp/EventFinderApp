using DAL;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using WpfApp1.Models;
using WpfApp1.Util;
using WpfApp1.ViewModels.Interfaces;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window, ILoginWindow
    {
        UserModel loggedUser;
        public Login(IDbCrud db)
        {
            InitializeComponent();
            
            DataContext = new LoginViewModel(this, db);
        }

        private void ButtonMinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public string GetPassword()
        {
            return pwdBox.Password;
        }
        public void Close(UserModel user, bool? dialogResult)
        {
            DialogResult = dialogResult;
            loggedUser = user;
            this.Close();
        }

        public UserModel GetLoggedUser()
        {
            return loggedUser;
        }
    }
}
