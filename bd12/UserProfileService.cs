using bd12.Data;
using bd12.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace bd12
{
    public class UserProfileService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public void Commit() => _db.SaveChanges();
    }
}
