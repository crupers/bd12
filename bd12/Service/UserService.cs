using bd12.Data;
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
        public UserService() {
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
            return excludeUserId.HasValue
                ? !_db.Users.Any(u => u.Email == email && u.Id != excludeUserId.Value)
                : !_db.Users.Any(u => u.Email == email);
        }
        public void Add(User user)
        {
            if (!IsLoginUnique(user.Login))
            {
                throw new InvalidOperationException($"Пользователь с логином '{user.Login}' уже существует");
            }
            var _user = new User
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                Date = user.Date,
            };
            _db.Add<User>(_user);
            Commit();
            User.Add(_user);
        }
        public int Commit() => _db.SaveChanges();
        public void GetAll()
        {
            var user = _db.Users.ToList();
            User.Clear();
            foreach(var item in user)
            {
                User.Add(item);
            }
        }
        public bool IsEmailUnique(string email)
        {
            return !_db.Users.Any(u => u.Email == email);
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
                Commit();
                GetAll();
            }
        }
        public void Remove(User user)
        {
            _db.Remove<User>(user);
            if(Commit() >0) if(User.Contains(user)) User.Remove(user);
        }
    }
}
