using bd12.Service;
using Microsoft.IdentityModel.Tokens;
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
   
    public partial class UserFormPage : Page
    {

        private UserService _service = new();
        public User _user = new();
        bool isEdit = false;
        public ObservableCollection<Role> Roles { get; set; }
     
        public UserFormPage(User? _editUser = null)
        {
            InitializeComponent();
            var roleService = new RoleService();
            Roles = RoleService.Roles;

            if (_editUser != null)
            {
                _user = _editUser;
                isEdit = true;
            }
           
             DataContext = _user;

        }
        private void save(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = LoginBox.Text?.Trim() ?? "";
                string email = EmailBox.Text?.Trim() ?? "";
                string password = PasswordBox.Text ?? "";
                string passwordRepeat = PasswordRepeatBox.Text ?? "";

            
                if (datepick.SelectedDate == null)
                {
                    MessageBox.Show("Выберите дату создания", "Ошибка");
                    return;
                }

                if (datepick.SelectedDate < DateTime.Now.Date)
                {
                    MessageBox.Show("Дата не может быть в прошлом");
                    return;
                }

             
                if (string.IsNullOrEmpty(login))
                {
                    MessageBox.Show("Логин не может быть пустым");
                    return;
                }

           
                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Email не может быть пустым");
                    return;
                }

                if (!email.Contains("@") || email.StartsWith("@"))
                {
                    MessageBox.Show("Введите корректный email адрес");
                    return;
                }

                if (!isEdit || !string.IsNullOrEmpty(password))
                {
                    if (string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Пароль не может быть пустым");
                        return;
                    }

                    if (password.Length < 8)
                    {
                        MessageBox.Show("Пароль должен содержать не менее 8 символов");
                        return;
                    }

                    if (!password.Any(char.IsDigit))
                    {
                        MessageBox.Show("Пароль должен содержать хотя бы одну цифру");
                        return;
                    }

                    if (!password.Any(char.IsLower))
                    {
                        MessageBox.Show("Пароль должен содержать хотя бы одну строчную букву");
                        return;
                    }

                    if (!password.Any(char.IsUpper))
                    {
                        MessageBox.Show("Пароль должен содержать хотя бы одну заглавную букву");
                        return;
                    }

                    if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                    {
                        MessageBox.Show("Пароль должен содержать хотя бы один специальный символ");
                        return;
                    }

                    if (password.Contains(" "))
                    {
                        MessageBox.Show("Пароль не должен содержать пробелов");
                        return;
                    }

                    if (password != passwordRepeat)
                    {
                        MessageBox.Show("Пароли не совпадают");
                        return;
                    }

                    _user.Password = password;
                }

           
                _user.Login = login;
                _user.Email = email;
                _user.Date = datepick.SelectedDate.Value;

             
                var selectedRole = RoleComboBox.SelectedItem as Role;
                if (selectedRole == null)
                {
                    MessageBox.Show("Выберите роль");
                    return;
                }

                _user.Role = selectedRole;
                _user.RoleId = selectedRole.Id;

          
                if (!_service.IsLoginUnique(login, isEdit ? _user.Id : (int?)null))
                {
                    MessageBox.Show($"Пользователь с логином '{login}' уже существует");
                    return;
                }

                if (!_service.IsEmailUnique(email, isEdit ? _user.Id : (int?)null))
                {
                    MessageBox.Show($"Пользователь с email '{email}' уже существует");
                    return;
                }

          
                if (isEdit)
                {
                    _service.Update(_user);
                    MessageBox.Show("Данные успешно обновлены");
                }
                else
                {
                    _service.Add(_user);
                    MessageBox.Show("Пользователь успешно добавлен");
                }

           
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();

                  
                    if (NavigationService.Content is MainPage mainPage)
                    {
                        mainPage.service.GetAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void editProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserProfileForm(_user));
        }
    }
}