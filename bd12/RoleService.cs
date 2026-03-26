using bd12.Data;
using bd12.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bd12
{
    public class RoleService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public static ObservableCollection<Role> Roles { get; set; } = new();
        public RoleService()
        {
            GetAll();
        }

        public void GetAll()
        {
            var roles = _db.Roles.ToList();
            Roles.Clear();
            foreach (var role in roles)
                Roles.Add(role);
        }
    }
}
