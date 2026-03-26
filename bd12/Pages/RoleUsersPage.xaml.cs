using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для RoleUsersPage.xaml
    /// </summary>
    public partial class RoleUsersPage : Page
    {
        public ObservableCollection<User> Users { get; set; } = new();
        public string RoleTitle { get; set; }
        public RoleUsersPage(Role role)
        {
            InitializeComponent();
            RoleTitle = $"{role.Title}";

            var service = new Service.UserService();
            var allUsers = service.User;

            foreach (var user in allUsers.Where(u => u.RoleId == role.Id))
            {
                Users.Add(user);
            }

            DataContext = this;
        }
        private void back(object sender,RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
