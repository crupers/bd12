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

namespace bd12.Pages
{
    /// <summary>
    /// Логика взаимодействия для RoleList.xaml
    /// </summary>
    public partial class RoleList : Page
    {
        public RoleService service { get; set; } = new();
        public Role? current { get; set; } = null;
        public RoleList()
        {
            InitializeComponent();
       
        }
        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void showUsers(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                NavigationService.Navigate(new RoleUsersPage(current));
            }
            else
            {
                MessageBox.Show("Выберите роль");
            }
        }
    }
}
