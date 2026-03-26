using bd12.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bd12.Service
{
    public class UserService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public ObservableCollection<User> User { get; set; } = new();

        public UserService()
        {
            GetAll();
        }

        public bool IsLoginUnique(string login, int? excludeUserId = null)
        {
            if (excludeUserId.HasValue)
            {
                return !_db.Users.Any(u => u.Login == login && u.Id != excludeUserId.Value);
            }
            return !_db.Users.Any(u => u.Login == login);
        }

        public bool IsEmailUnique(string email, int? excludeUserId = null)
        {
            if (excludeUserId.HasValue)
            {
                return !_db.Users.Any(u => u.Email == email && u.Id != excludeUserId.Value);
            }
            return !_db.Users.Any(u => u.Email == email);
        }

        public void Add(User user)
        {
            if (!IsLoginUnique(user.Login))
            {
                throw new InvalidOperationException($"Пользователь с логином '{user.Login}' уже существует");
            }

            if (user.Role != null && user.Role.Id > 0)
            {
                var existingRole = _db.Roles.Find(user.Role.Id);
                if (existingRole != null)
                {
                    user.Role = existingRole;
                    user.RoleId = existingRole.Id;
                }
            }

            _db.Users.Add(user);
            Commit();
            GetAll(); 
        }

        public int Commit() => _db.SaveChanges();

        public void GetAll()
        {
            var users = _db.Users
                .Include(u => u.Role)
                .Include(u => u.Profile)
                .ToList();

            User.Clear();
            foreach (var item in users)
            {
                User.Add(item);
            }
        }

        public void Update(User user)
        {
            if (!IsLoginUnique(user.Login, user.Id))
            {
                throw new InvalidOperationException($"Пользователь с логином '{user.Login}' уже существует");
            }

            var existingUser = _db.Users.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.Login = user.Login;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Date = user.Date;

                // Обновляем RoleId
                if (user.Role != null && user.Role.Id > 0)
                {
                    existingUser.RoleId = user.Role.Id;

                    // Загружаем роль, если нужно
                    var role = _db.Roles.Find(user.Role.Id);
                    if (role != null)
                    {
                        existingUser.Role = role;
                    }
                }

                Commit();
                GetAll();
            }
        }

        public void Remove(User user)
        {
            var userToDelete = _db.Users.Find(user.Id);
            if (userToDelete != null)
            {
                _db.Users.Remove(userToDelete);
                if (Commit() > 0)
                {
                    User.Remove(user);
                }
            }
        }
    }
}