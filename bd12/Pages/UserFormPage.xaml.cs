using bd12.Service;
using Microsoft.IdentityModel.Tokens;
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
   
    public partial class UserFormPage : Page
    {
        private UserService _service = new();
        public User _user = new();
        bool isEdit = false;
        public UserFormPage(User? _editUser = null)
        {
            InitializeComponent();
            if (_editUser != null) { 
            
            
            _user = _editUser;
                isEdit = true;
            }
            DataContext = _user;
        }
        private void save(object sender, RoutedEventArgs e)
        {
            string login = _user.Login?.Trim() ?? "";
            string email = _user.Email?.Trim() ?? "";
            string password = _user.Password ?? "";

            var passwordRepeatBox = FindName("PasswordRepeatBox") as TextBox;
            string passwordRepeat = passwordRepeatBox?.Text ?? "";

       if (datepick.SelectedDate < DateTime.Now)
            {
                MessageBox.Show("Дата не может быть в прошлом");
                return;
            }
            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Логин не может быть пустым", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
           
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email не может быть пустым", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!email.Contains("@") || email.StartsWith("@"))
            {
                MessageBox.Show("Введите корректный email адрес", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
          

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пароль не может быть пустым", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (password.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать не менее 8 символов", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну цифру", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!password.Any(char.IsLower))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну строчную букву", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну заглавную букву", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                MessageBox.Show("Пароль должен содержать хотя бы один специальный символ", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (password.Contains(" "))
            {
                MessageBox.Show("Пароль не должен содержать пробелов", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

          
            if (password != passwordRepeat)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

           
            if (_user.Date == DateTime.MinValue)
            {
                MessageBox.Show("Выберите дату создания", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            

        
            try
            {
                if (!_service.IsLoginUnique(login, isEdit ? _user.Id : (int?)null))
                {
                    MessageBox.Show($"Пользователь с логином '{login}' уже существует", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке логина: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (!_service.IsEmailUnique(email, isEdit ? _user.Id : (int?)null))
                {
                    MessageBox.Show($"Пользователь с email '{email}' уже существует", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке email: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            try
            {
                if (isEdit)
                {
                    _service.Update(_user);
                    MessageBox.Show("Данные успешно обновлены", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _service.Add(_user);
                    MessageBox.Show("Пользователь успешно добавлен", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}