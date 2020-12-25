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
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public NotificationWindow(List<WpfApp1.Models.EventModel> todaysEvents)
        {
            InitializeComponent();
            DataContext = new NotificationViewModel(todaysEvents);
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonMinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
