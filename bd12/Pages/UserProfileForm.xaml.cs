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
    /// Логика взаимодействия для UserProfileForm.xaml
    /// </summary>
    public partial class UserProfileForm : Page
    {
        private User _user;
        private UserProfileService _service = new();
        public UserProfileForm(User user)
        {
            InitializeComponent();
            _user = user;
            if (_user.Profile == null)
                _user.Profile = new UserProfile();

            DataContext = _user.Profile;
        }
        private void save(object sender, RoutedEventArgs e)
        {
            _service.Commit();
            NavigationService.GoBack();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
