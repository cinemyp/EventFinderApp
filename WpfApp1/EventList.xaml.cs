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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для EventList.xaml
    /// </summary>
    public partial class EventList : Page
    {
        public EventList()
        {
            InitializeComponent();
        }
        

        private void SignIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            login.Show();

        }
    }
}
